using System;
using System.IO;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Serilog;

namespace API
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var config = LoadConfiguration(args);
            //configure serilog as logger to the console and logfile
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(config.GetValue<string>("LoggerPath"))
            .Enrich.FromLogContext()
            .CreateLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try{
                    var context = services.GetRequiredService<DataContext>();
                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    context.Database.Migrate();
                    Seed.SeedData(context, userManager).Wait();//since SeedData is async and we are not inside an async method - we can use the Wait() method to get around that
                }
                catch(Exception ex){
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error has occured during migration");
                }
            }

            host.Run();
            // Important to call at exit so that batched events are flushed.
            Log.CloseAndFlush();
            Console.ReadKey(true);

        }

        static IConfiguration LoadConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            if (args != null)
            {
                builder.AddCommandLine(args);
            }
            return builder.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseKestrel(x => x.AddServerHeader = false)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
