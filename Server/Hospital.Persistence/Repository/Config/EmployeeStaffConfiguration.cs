using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;

namespace Hospital.Persistence.Repository.Config
{
    public class EmployeeStaffConfiguration : IEntityTypeConfiguration<EmployeeStaff>
    {
        public void Configure(EntityTypeBuilder<EmployeeStaff> builder)
        {
            // Table name
            builder.ToTable("EmployeeStaffs");

            // Primary Key
            builder.HasKey(e => e.EmpId);

            // Properties
            builder.Property(e => e.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Gender)
                   .IsRequired();

            builder.Property(e => e.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(e => e.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.Role)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.Salary)
                   .IsRequired();

            builder.Property(e => e.JoiningDate)
                   .IsRequired();

            builder.Property(e => e.Password)
                   .IsRequired();

            

            // Relationship (optional Department)
            builder.HasOne(e => e.Department)
                   .WithMany(d => d.Employees)
                   .HasForeignKey(e => e.DepartmentId)
                   .OnDelete(DeleteBehavior.SetNull); // If department is deleted, set to null
        }
    }
}
