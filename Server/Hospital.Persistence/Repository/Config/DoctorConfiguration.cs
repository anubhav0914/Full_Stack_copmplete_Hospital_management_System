using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;
using System;



namespace Hospital.Persistence.Repository.Config
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Table name
            builder.ToTable("Doctors");

            // Primary Key
            builder.HasKey(d => d.DoctorId);

            // Properties
            builder.Property(d => d.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(d => d.LastName)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(d => d.Specialization)
                .HasMaxLength(100);

            builder.Property(d => d.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired(true);

            builder.Property(d => d.Email)
                .HasMaxLength(100);

            builder.Property(d => d.Qualification)
                .HasMaxLength(100);

            builder.Property(d => d.ExperienceYear)
                .IsRequired(true);

            builder.Property(d => d.JoiningDate)
                .IsRequired(true);

            builder.Property(d => d.Availability)
                .HasConversion(
                    v => string.Join(',', v), // Store as "Monday,Wednesday"
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(day => Enum.Parse<DayOfWeek>(day))
                        .ToList()
        );

            builder.Property(d => d.Password)
                .IsRequired(true)
                .HasMaxLength(100);

          

            // Foreign Key (DepartmentId can be null)
            builder.HasOne(d => d.Department)
                .WithMany(dep => dep.Doctors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull); // Optional: NULL out department if department is deleted

            // Relationships
            builder.HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.BillingTransactions)
                .WithOne(b => b.Doctor)
                .HasForeignKey(b => b.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}