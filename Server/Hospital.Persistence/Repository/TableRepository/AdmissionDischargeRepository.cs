using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Persistence.Repository.TableRepository
{
    public class AdmissionDischargeRepository : HospitalRepository<AdmissionDischarge>, IAdmissionDischargeRepository
    {
        private readonly HospitalDBContext _dbContext;

        public AdmissionDischargeRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }
     
        


    }
}