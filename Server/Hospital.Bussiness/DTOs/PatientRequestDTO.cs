namespace Hospital.Bussiness.DTOs
{

    public class PatientRequestDTO
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
        public IFormFile Image { get; set; }


    }

}