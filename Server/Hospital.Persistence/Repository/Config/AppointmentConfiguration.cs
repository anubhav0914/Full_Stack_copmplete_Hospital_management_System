using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;



namespace Hospital.Persistence.Repository.Config
{
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        // Table name
        builder.ToTable("Appointments");

        // Primary Key
        builder.HasKey(a => a.AppointmentId);

        // Properties
        builder.Property(a => a.AppointmentDate)
            .IsRequired();

        builder.Property(a => a.AppTime)
            .IsRequired();

            // Foreign Key: Doctor
            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.SetNull); // Optional: Set DoctorId null if doctor is deleted

            // Foreign Key: Patient
            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .IsRequired(true)
            .OnDelete(DeleteBehavior.SetNull); // Optional: Set PatientId null if patient is deleted
    }
}
}