namespace Hpospital.Bussiness.Services.MailServices{

    public interface IMailService
    {
        Task<bool> SendEmailDoctorAsync(string toEmail, string password, string name);
        Task<bool> SendEmailEmployeeAsync(string toEmail, string password, string name, string jobRole);
        Task<bool> SendEmailPatientAsync(string toEmail, string name);
        Task<bool> SendOTPtoAdminAsync(string toEmail, string name,string otp);


    }
}