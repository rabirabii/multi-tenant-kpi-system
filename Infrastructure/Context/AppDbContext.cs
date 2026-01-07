using Core.Entities.Global;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            base.OnModelCreating(modelBuilder);

            // =========================
            // MUser
            // =========================
            modelBuilder.Entity<MUser>(entity =>
            {
                entity.ToTable("MUser", "public");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                      .HasColumnName("UserId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.Username)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.KeyCloakId)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);

                entity.Property(e => e.UpdatedAt)
                      .IsRequired(false);
            });

            // =========================
            // MTenant
            // =========================
            modelBuilder.Entity<MTenant>(entity =>
            {
                entity.ToTable("MTenant", "public");

                entity.HasKey(e => e.TenantId);

                entity.Property(e => e.TenantId)
                      .HasColumnName("TenantId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.TenantName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.SchemaName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(e => e.SchemaName)
                      .IsUnique();

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(true);
            });

            // =========================
            // MRole
            // =========================
            modelBuilder.Entity<MRole>(entity =>
            {
                entity.ToTable("MRole", "public");

                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                      .HasColumnName("RoleId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.RoleName)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(255);
            });

            // =========================
            // MGlobalMenu
            // =========================
            modelBuilder.Entity<MGlobalMenu>(entity =>
            {
                entity.ToTable("MGlobalMenu", "public");

                entity.HasKey(e => e.GlobalMenuId);

                entity.Property(e => e.GlobalMenuId)
                      .HasColumnName("GlobalMenuId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.MenuKey)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.HasIndex(e => e.MenuKey)
                      .IsUnique();

                entity.Property(e => e.DefaultLabel)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.DefaultRoute)
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Category)
                      .HasMaxLength(50)
                      .HasDefaultValue("General");

                entity.Property(e => e.Icon)
                      .HasMaxLength(50);
            });

            // =========================
            // TrTenantMenu
            // =========================
            modelBuilder.Entity<TrTenantMenu>(entity =>
            {
                entity.ToTable("TrTenantMenu", "public");

                entity.HasKey(e => e.TenantMenuId);

                entity.Property(e => e.TenantMenuId)
                      .HasColumnName("TenantMenuId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.CustomLabel)
                      .HasMaxLength(100);

                entity.Property(e => e.IsVisible)
                      .HasDefaultValue(true);

                entity.Property(e => e.SortOrder)
                      .HasDefaultValue(0);

                entity.HasOne(e => e.Tenant)
                      .WithMany()
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.GlobalMenu)
                      .WithMany()
                      .HasForeignKey(e => e.GlobalMenuId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =========================
            // TrUserTenant
            // =========================
            modelBuilder.Entity<TrUserTenant>(entity =>
            {
                entity.ToTable("TrUserTenant", "public");

                entity.HasKey(e => e.UserTenantId);

                entity.Property(e => e.UserTenantId)
                      .HasColumnName("UserTenantId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.HasOne(e => e.User)
                      .WithMany(e => e.UserTenants)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tenant)
                      .WithMany()
                      .HasForeignKey(e => e.TenantId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Role)
                      .WithMany()
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // =========================
            // TrRoleAccesses
            // =========================
            modelBuilder.Entity<TrRoleAccess>(entity =>
            {
                entity.ToTable("TrRoleAccess", "public");

                entity.HasKey(e => e.RoleAccessId);

                entity.Property(e => e.RoleAccessId)
                      .HasColumnName("RoleAccessId")
                      .HasDefaultValueSql("gen_random_uuid()");

                entity.Property(e => e.CanCreate).HasDefaultValue(false);
                entity.Property(e => e.CanRead).HasDefaultValue(false);
                entity.Property(e => e.CanUpdate).HasDefaultValue(false);
                entity.Property(e => e.CanDelete).HasDefaultValue(false);

                entity.HasOne(e => e.Role)
                      .WithMany()
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.TenantMenu)
                      .WithMany(e => e.RoleAccesses)
                      .HasForeignKey(e => e.TenantMenuId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
