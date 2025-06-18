using Hospital.Bussiness.DTOs;
using Hospital.Bussiness.Utils;

namespace Hospital.Bussiness.Services.AuthServices
{

    public interface ILoginServices
    {
        Task<LoginResponse> Login(LoginRequestDTO loginDTO);
        Task<LoginResponse> AdminLogin(AdminloginDTO loginDTO);
        Task<LoginResponse> AdminLoginOtpVerification(OTPDTO oTPDTO);

        


    }
}