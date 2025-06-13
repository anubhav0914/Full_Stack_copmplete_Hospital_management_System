using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IBillingTrasicationRepository : IHospitalRepository<BillingTransaction>
    {
        public Task<List<BillingTransaction>> GetBillByDoctorIDAsync(int id);
        public Task<List<BillingTransaction>> GetBillByPatientIDAsync(int id);
        public Task<List<BillingTransaction>> GetBillsyDateAsync(DateTime date);
        
    }
}