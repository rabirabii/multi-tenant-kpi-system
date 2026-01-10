using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configurations.Base
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> 
        where T : BaseAuditableEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreatedBy).HasMaxLength(50).IsRequired();
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.Property(e => e.UpdatedBy).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.UpdatedAt).IsRequired(false);

            builder.Property(e => e.DeletedBy).HasMaxLength(50).IsRequired(false);
            builder.Property(e => e.DeletedAt).IsRequired(false);

            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
