using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Identity;
using Domain;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace Test
{
    public class TestBase : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(ConfigureServices);
            builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole(options => options.IncludeScopes = true);
            });
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var config = new OpenIdConnectConfiguration()
                {
                    Issuer = MockJwtTokens.Issuer
                };

                config.SigningKeys.Add(MockJwtTokens.SecurityKey);
                options.Configuration = config;
            });
        }
     
        private readonly DataContext _context;
        private readonly DataContext _badContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRemoteJobAccessor _remoteAccessor;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<AppUser> _userManager;
        public TestBase(){
            _context = GetDbContext();
            _badContext = GetBadDbContext();
            var mockUserAccessor = new Mock<IUserAccessor>();
            _userAccessor = mockUserAccessor.Object;
            testContext =_context;
            testBadContext = _badContext;
            testMapper = _mapper;
            testUserAccessor = _userAccessor;
            var mockRemoteAccessor = new Mock<IRemoteJobAccessor>();
            Task<string> expectedResult = Task.FromResult<string>("Successfully Running Job");
            var mockUserManager = new Mock<UserManager<AppUser>>(new Mock<IUserStore<AppUser>>().Object,
                                                                new Mock<IOptions<IdentityOptions>>().Object,
                                                                new Mock<IPasswordHasher<AppUser>>().Object,
                                                                new IUserValidator<AppUser>[0],
                                                                new IPasswordValidator<AppUser>[0],
                                                                new Mock<ILookupNormalizer>().Object,
                                                                new Mock<IdentityErrorDescriber>().Object,
                                                                new Mock<IServiceProvider>().Object,
                                                                new Mock<ILogger<UserManager<AppUser>>>().Object);
            _userManager = mockUserManager.Object;
            testUserManager = _userManager;
            //mockRemoteAccessor.Setup(p => p.PostRemote()).Returns(expectedResult);
            //_remoteAccessor = mockRemoteAccessor.Object;
            testRemote = _remoteAccessor;
        }

        public DataContext testContext {get; set;}
        public DataContext testBadContext {get; set;}
        public IMapper testMapper {get; set;}
        public IUserAccessor testUserAccessor {get; set;}
        public IRemoteJobAccessor testRemote {get;set;}
        public IJwtGenerator testJwtGenerator { get; set; }
        public UserManager<AppUser> testUserManager { get; set; }
        public IProfileReader testProfileReader { get; set; }


        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
        
        private DataContext GetDbContext(bool useSqlite = false)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            if (useSqlite)
            {
                builder.UseSqlite("Data Source=:memory", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            
            var dbContext = new DataContext(builder.Options,_httpContextAccessor);
 
           if (useSqlite)
            {
               dbContext.Database.OpenConnection();
            }
 
            dbContext.Database.EnsureCreated();
 
            return dbContext;
        }

        private DataContext GetBadDbContext(bool useSqlite = true)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            if (useSqlite)
            {
                builder.UseSqlite("Data Source=:memory", x => { });
            }
            else
            {
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }
            
            var dbContext = new DataContext(builder.Options,_httpContextAccessor);
 
            return dbContext;
        }
    }
}