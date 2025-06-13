using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;
namespace Hospital.Persistence.Repository.Config
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {   
            builder.ToTable("Admins");
            builder.HasKey(a => a.AdminId);
            builder.HasIndex(a => a.Email).IsUnique();
            builder.Property(a => a.Username).IsRequired().HasMaxLength(100);
            builder.Property(a => a.PasswordHash).IsRequired();
        }
    }
}
