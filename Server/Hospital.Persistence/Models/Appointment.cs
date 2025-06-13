namespace Hospital.Persistence.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppTime { get; set; } 

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }

}