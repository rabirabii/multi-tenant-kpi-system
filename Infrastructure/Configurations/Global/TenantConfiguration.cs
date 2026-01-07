using Core.Entities.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace Infrastructure.Configurations.Global
{
    public class TenantConfiguration : IEntityTypeConfiguration<MTenant>
    {
        public void Configure(EntityTypeBuilder<MTenant> builder)
        {
            builder.ToTable("MTenant", "public");

            builder.HasKey(e => e.TenantId);

            builder.Property(e => e.TenantId)
                  .HasColumnName("TenantId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.TenantName)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(e => e.SchemaName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.HasIndex(e => e.SchemaName)
                  .IsUnique();

            builder.Property(e => e.IsActive)
                  .HasDefaultValue(true);
        }
    }
}
