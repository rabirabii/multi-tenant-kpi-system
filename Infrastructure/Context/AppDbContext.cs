using Core.Entities.Global;
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
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<MUser> Users { get; set; }
        public DbSet<MTenant> Tenants { get; set; }
        public DbSet<TrTenantMenu> TenantMenus { get; set; }
        public DbSet<TrRoleAccess> RoleAccesses { get; set; }
        public DbSet<MRole> Roles { get; set; }
        public DbSet<TrUserTenant> UserTenants { get; set; }
        public DbSet<MGlobalMenu> GlobalMenus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
