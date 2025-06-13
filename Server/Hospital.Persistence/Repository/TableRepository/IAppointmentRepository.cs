using Hospital.Persistence.Models;


namespace Hospital.Persistence.Repository.TableRepository
{
    public interface IAppointmentRepository : IHospitalRepository<Appointment>
    {
        public Task<List<Appointment>> GetAppointmentByDoctorIDAsync(int id);
        public Task<List<Appointment>> GetAppointmentByPatientIDAsync(int id);

        public Task<List<Appointment>> GetAppointmentsByDateAsync(DateTime date);
    }
}