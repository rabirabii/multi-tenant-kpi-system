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
    public class RoleAccessConfiguration : BaseEntityConfiguration<TrRoleAccess>
    {
        public override void Configure(EntityTypeBuilder<TrRoleAccess> builder) {
            builder.ToTable("TrRoleAccess", "public");

            builder.HasKey(e => e.RoleAccessId);

            builder.Property(e => e.RoleAccessId)
                  .HasColumnName("RoleAccessId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.CanCreate).HasDefaultValue(false);
            builder.Property(e => e.CanRead).HasDefaultValue(false);
            builder.Property(e => e.CanUpdate).HasDefaultValue(false);
            builder.Property(e => e.CanDelete).HasDefaultValue(false);

            builder.HasOne(e => e.Role)
                  .WithMany()
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.TenantMenu)
                  .WithMany(e => e.RoleAccesses)
                  .HasForeignKey(e => e.TenantMenuId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
