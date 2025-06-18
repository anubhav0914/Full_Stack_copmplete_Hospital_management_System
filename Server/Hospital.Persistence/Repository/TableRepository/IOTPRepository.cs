using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IOTPRepository : IHospitalRepository<OTPModel>
    {
        public Task<OTPModel> GetByEmail(string emial,string otp);

    }
}