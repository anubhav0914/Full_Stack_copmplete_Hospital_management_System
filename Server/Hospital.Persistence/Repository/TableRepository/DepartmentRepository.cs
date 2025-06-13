using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;

namespace Hospital.Persistence.Repository.TableRepository
{
    public class DepartmentRepository : HospitalRepository<Department>, IDepartmentRepository
    {
        private readonly HospitalDBContext _dbContext;

        public DepartmentRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<Department> GetByNameAsync(string departName)
        {
            var department =  _dbContext.Departments.Where(n => n.DepartmentName == departName).FirstOrDefault();
            return department;
        }


    }
}