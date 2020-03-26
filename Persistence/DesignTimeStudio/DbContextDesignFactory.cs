using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System.IO;

namespace Persistence.DesignTimeStudio
{
    public class DbContextDesignFactory : IDesignTimeDbContextFactory<DataContext>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public DbContextDesignFactory()
        {
        }

        public DbContextDesignFactory(IHttpContextAccessor httpContextAccessor, IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public DataContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false);
            var config = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            //To Create migration for Postgres SQL unmark the below

            var ConnectionStringBuilder = new NpgsqlConnectionStringBuilder(config.GetSection("EDRM:dbConnectionString").Value) //"Server =172.27.171.50;Port=5432;Database=EDRM;User Id=edradmin;")
            {
               Password = config.GetSection("EDRM:dbConnectionPassword").Value
            };

            optionsBuilder.UseNpgsql(ConnectionStringBuilder.ConnectionString);
            optionsBuilder.UseLazyLoadingProxies();
            //To Create migration for SQLITE unmark the below

            //optionsBuilder.UseSqlite(config.GetConnectionString("DefaultConnection"));// _configuration.GetConnectionString("DefaultConnection"));
            return new DataContext(optionsBuilder.Options, _httpContextAccessor);
        }
    }
}
