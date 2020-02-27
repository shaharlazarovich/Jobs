using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence
{
    public partial class DataContext: IdentityDbContext<AppUser>
    {


        public override int SaveChanges()
        {
            return SaveChangesAsyncAudit(true, CancellationToken.None).Result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return SaveChangesAsyncAudit(acceptAllChangesOnSuccess, CancellationToken.None).Result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return SaveChangesAsyncAudit(true, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            return SaveChangesAsyncAudit(acceptAllChangesOnSuccess, cancellationToken);
        }

        private async Task<int> SaveChangesAsyncAudit(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
        {
            if (!acceptAllChangesOnSuccess) //Ignore audit logging
                return await base.SaveChangesAsync(false, cancellationToken).ConfigureAwait(false);

            //Get the changes
            var entries = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Unchanged || e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted).ToList();
            var audits = new List<EntityEntryHolder>();
            //store them in holding area until after save
            foreach (var item in entries)
            {
                audits.Add(new EntityEntryHolder()
                {
                    ModType = item.State,
                    EntityEntry = item
                });
            }

            //Save changes
            int result = await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);

            string id;

            //the below line is risky - it introduces state into context - might impac the connection pooling mechnism - need
            //to check in case of performance impact
            //var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "system";
            var user = "system";
            if (_httpContextAccessor!=null)
                if (_httpContextAccessor.HttpContext!=null)
                    if (_httpContextAccessor.HttpContext.User!=null)
                        if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!= null)
                            user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "system";
            foreach (var item in audits)
            {
                id = GetIdentity(item.EntityEntry);
                var mType = item.ModType.ToString();
                var tName = item.EntityEntry.Entity.GetType().Name;
                var r = JsonConvert.SerializeObject(item.EntityEntry.Entity, new JsonSerializerSettings { Formatting = Formatting.None, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                this.Audits.Add(new Audit
                {
                    Login = user,
                    ModType = mType,
                    EntityName = tName,
                    EntityId = id,
                    Time = DateTime.Now,
                    RowAfter = r
                });
            }

            int logResult = await base.SaveChangesAsync(true, cancellationToken).ConfigureAwait(false);
            
            return result;
        }

        private string GetIdentity(EntityEntry entry)
        {
            string id = "";

            Type t = entry.Entity.GetType();

            var propInfo = t.GetProperties().FirstOrDefault(o => o.CustomAttributes.FirstOrDefault(oo => oo.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) != null);

            if (propInfo == null) //Fall back to Id Name
                propInfo = t.GetProperty("Id");

            if (propInfo != null)
                id = propInfo.GetValue(entry.Entity).ToString();

            return id;
        }
    }

    internal class EntityEntryHolder
    {
        public EntityState ModType { get; set; }
        public Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry EntityEntry { get; set; }
    }
}