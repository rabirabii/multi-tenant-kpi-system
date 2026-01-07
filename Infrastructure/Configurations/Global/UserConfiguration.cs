using Core.Entities.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configurations.Global
{
    public class UserConfiguration : IEntityTypeConfiguration<MUser>
    {
        public void Configure(EntityTypeBuilder<MUser> builder)
        {
            builder.ToTable("MUser", "public");

            builder.HasKey(e => e.UserId);

            builder.Property(e => e.UserId)
                  .HasColumnName("UserId")
                  .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(e => e.Username)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.Property(e => e.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(e => e.KeyCloakId)
                  .IsRequired();

            builder.Property(e => e.IsActive)
                  .HasDefaultValue(true);

            builder.Property(e => e.UpdatedAt)
                  .IsRequired(false);

        }
    }
}
