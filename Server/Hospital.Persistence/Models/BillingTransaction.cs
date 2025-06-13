namespace Hospital.Persistence.Models
{
    public class BillingTransaction
    {
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int TotalAmount { get; set; }
        public DateTime BillingDate { get; set; }
        public int DoctorId { get; set; }
        

        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
        public Doctor Doctor { get; set; }
    }

}