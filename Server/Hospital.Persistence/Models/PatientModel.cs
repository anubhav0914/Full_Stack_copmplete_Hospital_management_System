namespace Hospital.Persistence.Models
{

    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BloodGroup { get; set; }
        public string Password { get; set; }
        public string RefershToken { get; set; }

        public string ProfileImage { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<BillingTransaction> BillingTransactions { get; set; }
    }

}