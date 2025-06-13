using Hospital.Persistence.Repository;
using Hospital.Persistence.Models;
using Microsoft.EntityFrameworkCore;


namespace Hospital.Persistence.Repository.TableRepository
{
    public class AppointmentRepository : HospitalRepository<Appointment>, IAppointmentRepository
    {
        private readonly HospitalDBContext _dbContext;

        public AppointmentRepository(HospitalDBContext dBContext) : base(dBContext)
        {

            _dbContext = dBContext;
        }

        public async Task<List<Appointment>> GetAppointmentByDoctorIDAsync(int id)
        {

            var appointments = await _dbContext.Appointments
                    .Where(a => a.DoctorId == id)
                    .ToListAsync();

            return appointments;
        }
        public async Task<List<Appointment>> GetAppointmentByPatientIDAsync(int id)
        {
             var appointments = await _dbContext.Appointments
                    .Where(a => a.PatientId == id)
                    .ToListAsync();

            return appointments;
        }

        public async Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date)
        {
            var targetDate = date.Date;

            return await _dbContext.Appointments
                .Where(a => a.AppointmentDate.Date == targetDate)
                .ToListAsync();
        }




    }
}