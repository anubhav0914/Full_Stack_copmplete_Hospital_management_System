namespace Hospital.Bussiness.DTOs
{
    public class BillingTransactionDTO
    {
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int TotalAmount { get; set; }
        public DateTime BillingDate { get; set; }
        public int DoctorId { get; set; }

    }

}