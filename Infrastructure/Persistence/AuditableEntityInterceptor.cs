using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Claims;
using Core.Entities.Base;

namespace Infrastructure.Persistence
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditableEntityInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken)
        {
            UpdateAuditFields(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditFields(DbContext? context)
        {
            // Implementation for updating audit fields like CreatedBy, CreatedAt, etc.

            if (context == null) return;

            var user = _httpContextAccessor.HttpContext?.User?.FindFirst("preferred_username")?.Value ?? "SYSTEM";

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>()) { 
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = user;
                    entry.Entity.IsDeleted = false;
                }

                if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = user;

                    // Handle Soft Delete if the state is Deleted

                    if (entry.State == EntityState.Deleted)
                    {
                        entry.Entity.DeletedAt = DateTime.UtcNow;
                        entry.Entity.DeletedBy = user;
                        entry.Entity.IsDeleted = true;
                        entry.State = EntityState.Modified;
                    }
                }
            }
        }
    }
}
