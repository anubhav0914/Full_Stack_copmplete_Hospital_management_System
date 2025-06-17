using System;

namespace Hospital.Persistence.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public int ExperienceYear { get; set; }
        public DateTime JoiningDate { get; set; }
        public List<DayOfWeek> Availability { get; set; } = new List<DayOfWeek>();
        public int? DepartmentId { get; set; }
        public string Password { get; set; }

        public string ProfileImage { get; set; }

        public Department Department { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<BillingTransaction> BillingTransactions { get; set; }
    }

}