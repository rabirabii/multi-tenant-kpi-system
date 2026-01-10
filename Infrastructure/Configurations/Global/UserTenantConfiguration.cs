using Core.Entities.Global;
using Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace Infrastructure.Configurations.Global
{
    public class UserTenantConfiguration : BaseEntityConfiguration<TrUserTenant>
    {
        public override void Configure(EntityTypeBuilder<TrUserTenant> builder)
        {
            builder.ToTable("TrUserTenant", "public");

            builder.HasKey(e => e.UserTenantId);

            builder.Property(e => e.UserTenantId)
                  .HasColumnName("UserTenantId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.HasOne(e => e.User)
                  .WithMany(e => e.UserTenants)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Tenant)
                  .WithMany()
                  .HasForeignKey(e => e.TenantId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Role)
                  .WithMany()
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
