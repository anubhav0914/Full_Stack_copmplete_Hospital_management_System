using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;


namespace Hospital.Bussiness.Services
{

    public interface IPatientServices
    {

        Task<APIResponse<PatientDTO>> Register(PatientRequestDTO patient);
        Task<APIResponse<List<PatientDTO>>> GetAllPatient();

        Task<APIResponse<PatientDTO>> GetById(int id);
        Task<APIResponse<PatientDTO>> GetByEmail(string emial);


        Task<APIResponse<PatientDTO>> Update(PatientRequestDTO patient);

        Task<APIResponse<PatientDTO>> Delete(int id);

    }
}