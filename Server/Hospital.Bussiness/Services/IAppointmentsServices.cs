using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IAppointmentServices
    {

        public Task<APIResponse<AppointmentDTO>> AddAppointment(AppointmentRequestDTO appointment, string email);
        public Task<APIResponse<List<AppointmentDTO>>> GetAllAppointment();

        public Task<APIResponse<AppointmentDTO>> GetById(int id);

        public Task<APIResponse<AppointmentDTO>> Update(AppointmentRequestDTO department);

        public Task<APIResponse<AppointmentDTO>> Delete(int id);

        public Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByDoctorID(int id);
        public Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByPatientID(int id);
        public Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByDate(DateTime date);
        



    }
}