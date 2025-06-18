using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hospital.Persistence.Repository.Config
{
    public class OTPModelConfiguration : IEntityTypeConfiguration<OTPModel>
    {
        public void Configure(EntityTypeBuilder<OTPModel> builder)
        {
            builder.ToTable("OTPs");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(o => o.Otp)
                .IsRequired()
                .HasMaxLength(10); // assuming 6-digit or similar

            builder.Property(o => o.ExpiryTime)
                .IsRequired();

            builder.Property(o => o.IsUsed)
                .HasDefaultValue(false);
        }
    }
}
