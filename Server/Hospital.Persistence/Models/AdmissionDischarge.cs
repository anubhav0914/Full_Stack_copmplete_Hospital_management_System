using System;

namespace Hospital.Persistence.Models
{
    public class AdmissionDischarge
    {
        public int AdmitId { get; set; }

        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }

        public DateTime AdmissionDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public int RoomNo { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
