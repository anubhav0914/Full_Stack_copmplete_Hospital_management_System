using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Persistence.Repository.TableRepository
{
    public class EmployeeStaffRepository : HospitalRepository<EmployeeStaff>, IEmployeeStaffRepository
    {
        private readonly HospitalDBContext _dbContext;

        public EmployeeStaffRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<EmployeeStaff> GetByEmailAsync(string email)
        {
            var EmployeeStaff = _dbContext.EmployeeStaffs.Where(n => n.Email == email).FirstOrDefault();
            return EmployeeStaff;
        }

        public async Task<List<EmployeeStaff>> GetEmployeeByDepartmentIdAsync(int departmentId)
        {
            var result = await _dbContext.EmployeeStaffs
                .Where(d => d.DepartmentId == departmentId)
                .ToListAsync();
            return result;
        }

    }
}