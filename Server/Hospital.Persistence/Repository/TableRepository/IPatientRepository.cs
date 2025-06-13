using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IPatientRepository : IHospitalRepository<Patient>
    {
        public Task<Patient> GetByEmailAsync(string emial);

    }
}