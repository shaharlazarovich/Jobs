using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
 
namespace Application.Tests
{
    public class TestBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext GetDbContext(bool useSqlite = false)
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
    }
}