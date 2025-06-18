using System;

namespace Hospital.Persistence.Models
{
    public class OTPModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
        public bool IsUsed { get; set; }
    }
}
