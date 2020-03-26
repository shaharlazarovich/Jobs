using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using API.Helpers;
using Domain;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Logging;
using Persistence;
using Serilog;

namespace API
{
    public class Program
    {
        public static readonly Dictionary<string, string> _memoryDefaults =
        new Dictionary<string, string>
        {
            {"EDRM:PortConfiguration:ServicePort", "5000"},
            {"EDRM:PortConfiguration:WorkerProcessPort", "5001"},
            {"Logging:LogToEventLog", "false"},
        };
        
        public static void Main(string[] args)
        {
            //var host = CreateHostBuilder(args).Build();
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = CreateHostBuilder(args.Where(arg => arg != "--console").ToArray());

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }
            var config = LoadConfiguration(args);
            //configure serilog as logger to the console and logfile
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(config.GetValue<string>("LoggerPath"))
            .Enrich.FromLogContext()
            .CreateLogger();
            var host = builder.Build();

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

            if (isService)
            {
                host.RunAsService();
            }
            else
            {
                host.Run();
            }

            // Important to call at exit so that batched events are flushed.
            Log.CloseAndFlush();
            Console.ReadKey(true);

        }

        static IConfiguration LoadConfiguration(string[] args)
        {
            var PathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(PathToExe);
            var builder = new ConfigurationBuilder()
            .SetBasePath(pathToContentRoot)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            if (args != null)
            {
                builder.AddCommandLine(args);
            }
            return builder.Build();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    IHostEnvironment env = hostingContext.HostingEnvironment;
                    config.AddInMemoryCollection(_memoryDefaults);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddCommandLine(args);

                })
                //.UseIIS()
                .UseKestrel((context, options) =>
                {
                    var config = context.Configuration.GetSection("EDRM:PortConfiguration").Get<EDRMPortConfiguration>();
                    if (config.ListenOnlyOnLocalhost)
                    {
                        options.ListenLocalhost(config.ServicePort);
                    }
                    else
                    {
                        options.ListenAnyIP(config.ServicePort);
                    }
                })
                .UseSerilog()
                .UseStartup(typeof(Startup));
    }
}
