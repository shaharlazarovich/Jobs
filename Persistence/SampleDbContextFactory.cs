using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class SampleDbContextFactory : IDisposable
    {
        private DbConnection _connection;
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
                using (var context = new DataContext(options))
                {
                    context.Database.EnsureCreated();
                }
            }

            return new DataContext(CreateOptions());
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