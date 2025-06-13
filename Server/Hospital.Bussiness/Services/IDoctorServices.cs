using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IDoctorServices
    {

        Task<APIResponse<DoctorDTO>> RegisterDoctor(DoctorRequestDTO patient);
        Task<APIResponse<List<DoctorDTO>>> GetAllDoctors();

        Task<APIResponse<DoctorDTO>> GetById(int id);

        Task<APIResponse<DoctorDTO>> Update(DoctorRequestDTO patient);

        Task<APIResponse<DoctorDTO>> Delete(int id);

        Task<APIResponse<List<DayOfWeek>>> GetAvaibillity(int id);

        Task<APIResponse<List<DoctorDTO>>> GetDoctorsByDepartmentIdAsync(int departmentId);



    }
}