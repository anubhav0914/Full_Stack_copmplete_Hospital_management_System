using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Persistence.Repository.TableRepository
{
    public class BillingTrasicationRepository : HospitalRepository<BillingTransaction>, IBillingTrasicationRepository
    {
        private readonly HospitalDBContext _dbContext;

        public BillingTrasicationRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<List<BillingTransaction>> GetBillByDoctorIDAsync(int id)
        {

            var appointments = await _dbContext.BillingTransactions
                    .Where(a => a.DoctorId == id)
                    .ToListAsync();

            return appointments;
        }
        public async Task<List<BillingTransaction>> GetBillByPatientIDAsync(int id)
        {
            var appointments = await _dbContext.BillingTransactions
                   .Where(a => a.PatientId == id)
                   .ToListAsync();

            return appointments;
        }

        public async Task<List<BillingTransaction>> GetBillsyDateAsync(DateTime date)
        {
            var targetDate = date.Date;

            return await _dbContext.BillingTransactions
                .Where(a => a.BillingDate.Date == targetDate)
                .ToListAsync();
        }

    }
}