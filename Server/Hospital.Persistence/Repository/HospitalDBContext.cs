using Microsoft.EntityFrameworkCore;
using Hospital.Persistence.Models;
using Hospital.Persistence.Repository.Config;
using BCrypt.Net;
namespace Hospital.Persistence.Repository
{
    public class HospitalDBContext : DbContext
    {

        public HospitalDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<BillingTransaction> BillingTransactions { get; set; }
        public DbSet<AdmissionDischarge> AdmissionDischarges { get; set; }

        public DbSet<EmployeeStaff> EmployeeStaffs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<OTPModel> OtpEntries { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new PatientConfiguration());
            modelBuilder.ApplyConfiguration(new DoctorConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new BillingTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new AdmissionDischargeConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeStaffConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new OTPModelConfiguration());


            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminId = 1,
                    Username = "anubhavGupta",
                    Email = "anubhavg.csce22@gmail.com",
                    PasswordHash = "$2a$12$6tAoLXokSm2FgdSegUhmouXBrGtzx3aS4zKGDsjP9XooCaRIxoMmO", // Anubhav@1805
                },
                new Admin
                {
                    AdminId = 2,
                    Username = "pucchu",
                    Email = "pucchu.csce22@gmail.com",
                    PasswordHash = "$2a$12$7RE.99TIQKIQZzJwli0YL.etFh3Y8QG1QTQUQvEwORv5TXjP0eaNa" // Pucchu@0914
                });



        }
    }
}