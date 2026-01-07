using Core.Entities.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using static Dapper.SqlMapper;

namespace Infrastructure.Configurations.Global
{
    public class RoleConfiguration : IEntityTypeConfiguration<MRole>
    {
        public void Configure(EntityTypeBuilder<MRole> builder)
        {
            builder.ToTable("MRole", "public");

            builder.HasKey(e => e.RoleId);

            builder.Property(e => e.RoleId)
                  .HasColumnName("RoleId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.RoleName)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(e => e.Description)
                  .HasMaxLength(255);
        }
    }
}
