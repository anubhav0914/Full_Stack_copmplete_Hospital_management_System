using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;

namespace Hospital.Persistence.Repository.TableRepository
{
    public class AdminRepository : HospitalRepository<Admin>, IAdminRepository
    {
        private readonly HospitalDBContext _dbContext;

        public AdminRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<Admin> GetByEmailAsync(string email)
        {
            var Admin =   _dbContext.Admins.Where(n => n.Email == email).FirstOrDefault();
            return Admin;
        }


    }
}