using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{

    public partial class DataContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions options,IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Audit> Audits { get; set; }
    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Value>()
                .HasData(
                    new Value { Id = 1, Name = "Value 101" },
                    new Value { Id = 2, Name = "Value 102" },
                    new Value { Id = 3, Name = "Value 103" }
                );
            
        }
    }
}
