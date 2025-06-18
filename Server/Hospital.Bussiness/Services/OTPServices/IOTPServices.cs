using System.Threading.Tasks;

namespace Hospital.Bussiness.Services.OTPServices
{
    public interface IOTPService
    {
        Task<string> GenerateOtpAsync(string email,string name);
        Task<bool> VerifyOtpAsync(string email, string otp);
    }
}
