using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IAdminRepository : IHospitalRepository<Admin>
    {
         Task<Admin> GetByEmailAsync(string email);

    }
}