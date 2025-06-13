using System;

namespace Hospital.Bussiness.DTOs
{
    public class AdmissionDischargeDTO
    {
        public int AdmitId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }

        public DateTime DischargeDate { get; set; }
        public DateTime AdmissionDate { get; set; }

        public int RoomNo { get; set; }

    }
}
