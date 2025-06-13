using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;

namespace Hospital.Persistence.Repository.Config
{
    public class AdmissionDischargeConfiguration : IEntityTypeConfiguration<AdmissionDischarge>
    {
        public void Configure(EntityTypeBuilder<AdmissionDischarge> builder)
        {
            // Table name
            builder.ToTable("AdmissionDischarges");

            // Primary Key
            builder.HasKey(ad => ad.AdmitId);

            // Required fields
            builder.Property(ad => ad.AdmissionDate)
                   .IsRequired();

            builder.Property(ad => ad.DischargeDate)
                   .IsRequired();

            builder.Property(ad => ad.RoomNo)
                   .IsRequired();

            // Foreign key: Doctor (nullable) with Restrict behavior
            builder.HasOne(ad => ad.Doctor)
                   .WithMany()
                   .HasForeignKey(ad => ad.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Foreign key: Patient (nullable) with Restrict behavior
            builder.HasOne(ad => ad.Patient)
                   .WithMany()
                   .HasForeignKey(ad => ad.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
