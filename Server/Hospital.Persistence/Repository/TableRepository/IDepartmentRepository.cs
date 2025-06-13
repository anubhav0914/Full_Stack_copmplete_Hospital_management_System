using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IDepartmentRepository : IHospitalRepository<Department>
    {
        public Task<Department> GetByNameAsync(string departName);
    }
}