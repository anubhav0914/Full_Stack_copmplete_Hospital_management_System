using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IDoctorRepository : IHospitalRepository<Doctor>
    {
        public Task<Doctor> GetByEmailAsync(string emial);
        public  Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId);
    }
}