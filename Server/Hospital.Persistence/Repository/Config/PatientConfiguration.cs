using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // Table Name
        builder.ToTable("Patients");

        // Primary Key
        builder.HasKey(p => p.PatientId);

        // Properties
        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Gender)
            .HasMaxLength(10);

        builder.Property(p => p.AddressLine1)
            .HasMaxLength(200);

        builder.Property(p => p.AddressLine2)
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(p => p.Email)
            .HasMaxLength(100);

        builder.Property(p => p.BloodGroup)
            .HasMaxLength(5)
            .IsRequired(false);

        builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.RefershToken)
            .HasMaxLength(500)
            .IsRequired(false);

      

        builder.Property(p => p.AdmissionDate)
            .IsRequired();

        builder.Property(p => p.CreatedDate)
            .IsRequired();

        builder.Property(p => p.UpdatedDate)
            .IsRequired();

        builder.Property(p => p.ProfileImage)
                .IsRequired(false)
                .HasMaxLength(100);

        // Relationships
        builder.HasMany(p => p.Appointments)
            .WithOne(a => a.Patient)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.BillingTransactions)
            .WithOne(b => b.Patient)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
