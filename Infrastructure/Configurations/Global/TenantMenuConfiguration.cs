using Core.Entities.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configurations.Global
{
    public class TenantMenuConfiguration : IEntityTypeConfiguration<TrTenantMenu>
    {
        public void Configure(EntityTypeBuilder<TrTenantMenu> builder)
        {
            builder.ToTable("TrTenantMenu", "public");

            builder.HasKey(e => e.TenantMenuId);

            builder.Property(e => e.TenantMenuId)
                  .HasColumnName("TenantMenuId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.CustomLabel)
                  .HasMaxLength(100);

            builder.Property(e => e.IsVisible)
                  .HasDefaultValue(true);

            builder.Property(e => e.SortOrder)
                  .HasDefaultValue(0);

            builder.HasOne(e => e.Tenant)
                  .WithMany()
                  .HasForeignKey(e => e.TenantId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.GlobalMenu)
                  .WithMany()
                  .HasForeignKey(e => e.GlobalMenuId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
