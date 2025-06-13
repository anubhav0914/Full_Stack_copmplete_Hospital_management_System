namespace Hospital.Bussiness.DTOs
{
    public class AppointmentRequestDTO
    {   
        
        public int AppoinmentId { get; set; }
        public int DoctorId { get; set; }
        // public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppTime { get; set; } 

        
    }

}