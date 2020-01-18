using API.Middleware;
using Application.Jobs;
using Application.Interfaces;
using Domain;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Security;
using Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using AutoMapper;
using Infrastructure.Photos;
using Infrastructure.CSV;
using Infrastructure.Kafka;
using System.Threading.Tasks;
using Application.Profiles;
using System;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //when we inject our Configuration we have access to our app.settings as well as our user-secrets
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
                //opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //opt.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                //opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                //opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //opt.UseMySql(Configuration.GetConnectionString("DefaultConnection"));
                opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            ConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => { //this will allow our cross-origin (domain) requests instead of using a proxy like nginx or using jsonp
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("www-authenticate")
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials(); //this is needed specifically for signalR
                });
            });
            services.AddMediatR(typeof(List.Handler).Assembly);//we're gonna use CQRS - command/query responsibility segragation - currently with mediator pattern later with event sourcing
            services.AddAutoMapper(typeof(List.Handler));
            services.AddControllers(opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Create>());
            // the below is the configuration of aspnetcore identity
            var builder = services.AddIdentityCore<AppUser>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"]));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateAudience = false,
                            ValidateIssuer = false,
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        }; 
                        //the below is our special treatment to pass on the jwt token
                        //via the signalR message - which is not http based
                        opt.Events = new JwtBearerEvents {
                            OnMessageReceived = context => {
                                var accessToken = context.Request.Query["access_token"];
                                var path = context.HttpContext.Request.Path;
                                if (!string.IsNullOrEmpty(accessToken) &&
                                    (path.StartsWithSegments("/chat"))) {
                                        context.Token = accessToken;
                                    }
                                    return Task.CompletedTask;
                            }
                        };
                    }
                );
            //the below services.AddScoped allow us to inject the classes based on the below interfaces
            //into our application logic when we need to
            //in .net dependency injection there are 3 types of lifetimes:
            //Transient objects are always different; a new instance is provided to every controller and every service.
            //Scoped objects are the same within a request, but different across different requests.
            //Singleton objects are the same for every object and every request.
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IProfileReader, ProfileReader>();
            services.AddScoped<ICsvAccessor, CsvAccessor>();
            services.AddScoped<IKafkaConsumerAccessor, KafkaConsumer>();
            services.AddScoped<IKafkaProducerAccessor, KafkaProducer>();

            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));
            services.Configure<KafkaSettings>(Configuration.GetSection("Kafka"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>(); //this is a generic error handler between client and server that logs the errors in between

            if (!env.IsDevelopment())
                app.UseHsts(); //for sending https headers only
            //the order of the below lines is important - we need to place the routing/corspolicy/authentication/authorization etc. in the correct order
            //error handling must always be first - otherwise errors coming from routing etc. won't be logged
            
            //below are security headers
            app.UseXContentTypeOptions(); //prevent content type sniffing

            app.UseReferrerPolicy(opt => opt.NoReferrer());//limit the amount of information passed on to other sites when referring to other sites

            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());//prevent cross site scripting

            app.UseXfo(opt => opt.Deny()); //prevent being iframed and click jacking

            app.UseCsp(opt => opt //this is our content security policy - basically saying, all content will be served from our own servers
                .BlockAllMixedContent()
                .StyleSources(s => s.Self()
                    .CustomSources("https://fonts.googleapis.com") //we are adding exception for external sources embedded in our code                           
                )
                .FontSources(s => s.Self()
                    .CustomSources("https://fonts.gstatic.com", "data:") //we are adding exception for external sources embedded in our code
                )
                .FormActions(s => s.Self())
                .FrameAncestors(s => s.Self())
                .ImageSources(s => s.Self()
                    .CustomSources("https://res.cloudinary.com", "blob:", "data:") //we are adding exception for external sources embedded in our code
                )
                .ScriptSources(s => s.Self()
                    .CustomSources("sha256-zTmokOtDNMlBIULqs//ZgFtzokerG72Q30ccMjdGbSA=") //we are adding exception for external sources embedded in our code
                )
            );
            app.UseDefaultFiles(); //this will look in wwwroot folder for files like index.html, default.html etc.

            app.UseStaticFiles();//this will serve from our .net app all the static react content in wwwroot folder

            app.UseRouting();

            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("index", "Fallback");//this will allow .net core to pass on every route it doesn't recongnize to a new fallback controller,
                                                //which will basically pass these routes to react to handle
            });
        }
    }
}
