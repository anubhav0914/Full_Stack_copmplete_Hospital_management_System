using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IEmployeeStaffRepository : IHospitalRepository<EmployeeStaff>
    {
        public Task<EmployeeStaff> GetByEmailAsync(string emial);
        public  Task<List<EmployeeStaff>> GetEmployeeByDepartmentIdAsync(int departmentId);
        
    }
}