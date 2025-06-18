using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Persistence.Repository.TableRepository
{
    public class OTPRepository : HospitalRepository<OTPModel>, IOTPRepository
    {
        private readonly HospitalDBContext _dbContext;

        public OTPRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<OTPModel> GetByEmail(string email,string otp)
        {
            var otps = await _dbContext.OtpEntries
                .Where(x => x.Email == email && x.Otp == otp && !x.IsUsed && x.ExpiryTime > DateTime.UtcNow)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            return otps;
            // return new OTPModel();
        }


    }
}