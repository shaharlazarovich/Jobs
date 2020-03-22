using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{

    public partial class DataContext : IdentityDbContext<AppUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<JobAction> JobActions { get; set; }
        public DbSet<JobSummaryResult> JobSummaryResults { get; set; }
        public DbSet<JobDeviceResult> JobDeviceResults { get; set; }
        public DbSet<JobTestResult> JobTestResults { get; set; }
        public DbSet<DRTest> DRTests {get; set;}
        public DbSet<Device> Devices { get; set; }
        public DbSet<TrackEvent> TrackEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("EDRM");
            base.OnModelCreating(builder);

            builder.Entity<Job>()
               .Property(j => j.Id)
                .ValueGeneratedOnAdd();
            
            builder.Entity<Job>()
                .HasMany(j => j.Devices)
                .WithOne();

            builder.Entity<Job>()
                .HasMany(j => j.JobSummaryResults)
                .WithOne();

            builder.Entity<Audit>() //multi-column non-clustered index 
                .HasIndex(a => new { a.EntityName, a.EntityId });

            builder.Entity<Audit>() //multi-column non-clustered index 
                .HasIndex(a => new { a.EntityName, a.Time });

            builder.Entity<Audit>() //single-column non-clustered index 
                .HasIndex(a => a.Time);

            builder.Entity<JobAction>() //auto increment id
                .Property(ja => ja.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<DRTest>()
                .Property(drTest => drTest.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<JobSummaryResult>()
                .Property(j => j.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<JobDeviceResult>()
                .HasMany(jd => jd.JobTestResults)
                .WithOne();
            
            builder.Entity<JobDeviceResult>()
                .Property(j => j.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<JobTestResult>()
                .Property(j => j.Id)
                .ValueGeneratedOnAdd();
            
            builder.Entity<Device>()
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Device>()
                .HasMany(d => d.DRTests)
                .WithOne();
        }
    }
}
