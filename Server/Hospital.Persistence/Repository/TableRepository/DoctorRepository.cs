using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Persistence.Repository.TableRepository
{
    public class DoctorRepository : HospitalRepository<Doctor>, IDoctorRepository
    {
        private readonly HospitalDBContext _dbContext;

        public DoctorRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<Doctor> GetByEmailAsync(string email)
        {
            var Doctor = _dbContext.Doctors.Where(n => n.Email == email).FirstOrDefault();
            return Doctor;
        }

        public async Task<List<Doctor>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            return await _dbContext.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .ToListAsync();
        }

    }
}