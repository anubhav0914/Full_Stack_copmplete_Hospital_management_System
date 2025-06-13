using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.Config
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            // Table name
            builder.ToTable("Departments");

            // Primary Key
            builder.HasKey(d => d.DepartmentId);

            // Properties
            builder.Property(d => d.DepartmentName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.DepartmentHead)
                   .HasMaxLength(100);

            builder.Property(d => d.CreationDate)
                   .IsRequired();

            builder.Property(d => d.NoOfEmployees)
                   .IsRequired();

            // Relationships

            builder.HasMany(d => d.Doctors)
                   .WithOne(doc => doc.Department)
                   .HasForeignKey(doc => doc.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(d => d.Employees)
                   .WithOne(emp => emp.Department)
                   .HasForeignKey(emp => emp.DepartmentId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
