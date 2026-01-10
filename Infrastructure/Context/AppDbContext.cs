using Core.Entities.Global;
using Infrastructure.Interceptors;
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
