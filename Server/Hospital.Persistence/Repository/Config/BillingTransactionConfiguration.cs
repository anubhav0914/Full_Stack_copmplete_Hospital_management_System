using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;



namespace Hospital.Persistence.Repository.Config
{
public class BillingTransactionConfiguration : IEntityTypeConfiguration<BillingTransaction>
{
    public void Configure(EntityTypeBuilder<BillingTransaction> builder)
    {
        // Table name
        builder.ToTable("BillingTransactions");

        // Primary Key
        builder.HasKey(b => b.BillId);

        // Properties
        builder.Property(b => b.TotalAmount)
            .IsRequired();

        builder.Property(b => b.BillingDate)
            .IsRequired();

        // Foreign Key: Patient
        builder.HasOne(b => b.Patient)
            .WithMany(p => p.BillingTransactions)
            .HasForeignKey(b => b.PatientId)
            .OnDelete(DeleteBehavior.SetNull);

            // Foreign Key: Doctor
            builder.HasOne(b => b.Doctor)
                .WithMany(d => d.BillingTransactions)
                .HasForeignKey(b => b.DoctorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.SetNull);

        // Foreign Key: Appointment
        builder.HasOne(b => b.Appointment)
            .WithMany()
            .HasForeignKey(b => b.AppointmentId)
            
            .OnDelete(DeleteBehavior.SetNull);
    }
}
}