using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;

namespace Hospital.Persistence.Repository.TableRepository
{
    public class PatientRepository : HospitalRepository<Patient>, IPatientRepository
    {
        private readonly HospitalDBContext _dbContext;

        public PatientRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<Patient> GetByEmailAsync(string email)
        {
            var patient =  _dbContext.Patients.Where(n => n.Email == email).FirstOrDefault();
            return patient;
        }


    }
}