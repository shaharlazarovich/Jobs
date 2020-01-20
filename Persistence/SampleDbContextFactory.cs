using System;
using System.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SampleDbContextFactory : IDisposable
    {
        private DbConnection _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private DbContextOptions<DataContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(_connection).Options;
        }

        public DataContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection("DataSource=:memory:");
                _connection.Open();

                var options = CreateOptions();
                using (var context = new DataContext(options,_httpContextAccessor))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new DataContext(CreateOptions(),_httpContextAccessor);
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}