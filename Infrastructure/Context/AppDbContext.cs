using Core.Entities.Base;
using Core.Entities.Global;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        private readonly AuditableEntityInterceptor _auditableEntityInterceptor;
        public AppDbContext(DbContextOptions<AppDbContext> options, 
            AuditableEntityInterceptor AuditableEntityInterceptor
            ) : base(options)
        {
            _auditableEntityInterceptor = AuditableEntityInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<MUser> Users { get; set; }
        public DbSet<MTenant> Tenants { get; set; }
        public DbSet<TrTenantMenu> TenantMenus { get; set; }
        public DbSet<TrRoleAccess> RoleAccesses { get; set; }
        public DbSet<MRole> Roles { get; set; }
        public DbSet<TrUserTenant> UserTenants { get; set; }
        public DbSet<MGlobalMenu> GlobalMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "p");
                    var deletedProperty = System.Linq.Expressions.Expression.Property(parameter, "IsDeleted");
                    var condition = System.Linq.Expressions.Expression.Equal(deletedProperty, System.Linq.Expressions.Expression.Constant(false));
                    var lambda = System.Linq.Expressions.Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
