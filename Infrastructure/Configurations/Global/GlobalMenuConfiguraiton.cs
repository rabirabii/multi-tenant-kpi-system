using Core.Entities.Global;
using Infrastructure.Configurations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations.Global
{
    public class GlobalMenuConfiguraiton : BaseEntityConfiguration<MGlobalMenu>
    {
        public override void Configure(EntityTypeBuilder<MGlobalMenu> builder)
        {
            builder.ToTable("MGlobalMenu", "public");
            builder.HasKey(e => e.GlobalMenuId);
            builder.Property(e => e.GlobalMenuId)
                  .HasColumnName("GlobalMenuId")
                  .HasDefaultValueSql("gen_random_uuid()");
            builder.Property(e => e.MenuKey)
                  .HasMaxLength(50)
                  .IsRequired();
            builder.HasIndex(e => e.MenuKey)
                  .IsUnique();
            builder.Property(e => e.DefaultLabel)
                  .HasMaxLength(100)
                  .IsRequired();
            builder.Property(e => e.DefaultRoute)
                  .HasMaxLength(255)
                  .IsRequired();
            builder.Property(e => e.Category)
                  .HasMaxLength(50)
                  .HasDefaultValue("General");
            builder.Property(e => e.Icon)
                  .HasMaxLength(100);
         
        }
    }
}
