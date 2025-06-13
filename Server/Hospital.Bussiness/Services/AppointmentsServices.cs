using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Repository.TableRepository;
using Hospital.Bussiness.Services;
using System;

namespace Hospital.Bussiness.Services
{

    public class AppointmentServices : IAppointmentServices
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        public AppointmentServices(
            IAppointmentRepository appointmentRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository
            )
        {
            _appointmentRepository = appointmentRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<APIResponse<AppointmentDTO>> AddAppointment(AppointmentRequestDTO appointment, string email)
        {
            var patient = await _patientRepository.GetByEmailAsync(email);
            if (patient == null)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "Unauthorized",
                    Data = null
                };
            }
            var doctor = await _doctorRepository.GetById(appointment.DoctorId);
            if (doctor == null)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No doctor found with this id",
                    Data = null
                };
            }

            var availability = doctor.Availability;
            DayOfWeek today = appointment.AppointmentDate.DayOfWeek;
            bool isAvailableToday = availability.Contains(today);
            Console.Write(isAvailableToday);
            Console.Write(today);


            if (!isAvailableToday)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "The doctor is not available on this date",
                    Data = null
                };
            }

            var appointmentsOnDate = await _appointmentRepository
               .GetAllAsync(a => a.DoctorId == appointment.DoctorId
                           && a.AppointmentDate.Date == appointment.AppointmentDate.Date);

            bool timeConflict = appointmentsOnDate.Any(a => a.AppTime == appointment.AppTime);

            if (timeConflict)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "This time slot is already booked, choose another one",
                    Data = null
                };
            }

            var newAppointment = new Appointment
            {
                DoctorId = appointment.DoctorId,
                PatientId = patient.PatientId,
                AppointmentDate = appointment.AppointmentDate.Date,
                AppTime = appointment.AppTime,
            };

            var added = await _appointmentRepository.AddAsync(newAppointment);

            if (!added)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "Somthing went wrong while adding appointmnet ",
                    Data = null
                };
            }

            return new APIResponse<AppointmentDTO>
            {
                Status = true,
                StatusCode = 409,
                Message = "Your appointment has been booked",
                Data = null
            };


        }
        public async Task<APIResponse<List<AppointmentDTO>>> GetAllAppointment()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            if (appointments == null || !appointments.Any())
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var appointmnetsDTOs = appointments.Select(p => new AppointmentDTO
            {
                AppointmentId = p.AppointmentId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                AppointmentDate = p.AppointmentDate.Date,
                AppTime = p.AppTime

            }).ToList();

            return new APIResponse<List<AppointmentDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = appointmnetsDTOs
            };

        }

        public async Task<APIResponse<AppointmentDTO>> GetById(int id)
        {
            var appointment = await _appointmentRepository.GetById(id);

            if (appointment == null)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Appoinment found with this Id",
                    Data = null
                };
            }

            var appoinmnetDTOs = new AppointmentDTO
            {

                AppointmentId = appointment.AppointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                AppointmentDate = appointment.AppointmentDate.Date,
                AppTime = appointment.AppTime
            };

            return new APIResponse<AppointmentDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = appoinmnetDTOs
            };
        }

        public async Task<APIResponse<AppointmentDTO>> Update(AppointmentRequestDTO appointment)
        {
            var doctor = await _doctorRepository.GetById(appointment.DoctorId);
            var existingAppointmnet = await _appointmentRepository.GetById(appointment.AppoinmentId);
            if (doctor == null)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No doctor found with this id",
                    Data = null
                };
            }

            var availability = doctor.Availability;
            DayOfWeek today = DateTime.UtcNow.DayOfWeek;
            bool isAvailableToday = availability.Contains(today);

            if (isAvailableToday)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "The doctor is not available on this date",
                    Data = null
                };
            }

            var appointmentsOnDate = await _appointmentRepository
               .GetAllAsync(a => a.DoctorId == appointment.DoctorId
                           && a.AppointmentDate.Date == appointment.AppointmentDate.Date);

            bool timeConflict = appointmentsOnDate.Any(a => a.AppTime == appointment.AppTime);

            if (timeConflict)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "This time slot is already booked, choose another one",
                    Data = null
                };
            }


            existingAppointmnet.DoctorId = appointment.DoctorId;
            existingAppointmnet.AppointmentDate = appointment.AppointmentDate.Date;
            existingAppointmnet.AppTime = appointment.AppTime;

            var added = await _appointmentRepository.UpdateAsync(existingAppointmnet);

            if (!added)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 409,
                    Message = "Somthing went wrong while adding appointmnet ",
                    Data = null
                };
            }

            return new APIResponse<AppointmentDTO>
            {
                Status = true,
                StatusCode = 409,
                Message = "Your appointment has been updated",
                Data = null
            };

        }

        public async Task<APIResponse<AppointmentDTO>> Delete(int id)
        {
            try
            {
                var existingAppointment = await _appointmentRepository.GetById(id);

                if (existingAppointment == null)
                {
                    return new APIResponse<AppointmentDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No appointment found with the given ID",
                        Data = null
                    };
                }

                var result = await _appointmentRepository.DeleteAsync(id);

                if (result)
                {
                    return new APIResponse<AppointmentDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Appointmnet deleted successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<AppointmentDTO>
                    {
                        Status = false,
                        StatusCode = 500,
                        Message = "Something went wrong while deleting the data",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new APIResponse<AppointmentDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByDoctorID(int id)
        {
            var appointments = await _appointmentRepository.GetAppointmentByDoctorIDAsync(id);

            if (appointments == null)
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var appointmentsDTOs = appointments.Select(p => new AppointmentDTO
            {
                AppointmentId = p.AppointmentId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                AppointmentDate = p.AppointmentDate.Date,
                AppTime = p.AppTime

            }).ToList();
             if (appointmentsDTOs == null || !appointmentsDTOs.Any())
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "no data found",
                    Data = null
                };
            }
            return new APIResponse<List<AppointmentDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = appointmentsDTOs
            };



        }
        public async Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByPatientID(int id)
        {
            var appointments = await _appointmentRepository.GetAppointmentByPatientIDAsync(id);

            if (appointments == null || !appointments.Any())
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var appointmentsDTOs = appointments.Select(p => new AppointmentDTO
            {
                AppointmentId = p.AppointmentId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                AppointmentDate = p.AppointmentDate.Date,
                AppTime = p.AppTime

            }).ToList();
            if (appointmentsDTOs == null || !appointmentsDTOs.Any())
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "no data found",
                    Data = null
                };
            }
            return new APIResponse<List<AppointmentDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = appointmentsDTOs
            };

        }

        public async Task<APIResponse<List<AppointmentDTO>>> GetAppointmentByDate(DateTime date)
        {
            var appointments = await _appointmentRepository.GetAppointmentsByDateAsync(date);

            if (appointments == null)
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var appointmentsDTOs = appointments.Select(p => new AppointmentDTO
            {
                AppointmentId = p.AppointmentId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                AppointmentDate = p.AppointmentDate.Date,
                AppTime = p.AppTime

            }).ToList();

            if (appointmentsDTOs == null || !appointmentsDTOs.Any())
            {
                return new APIResponse<List<AppointmentDTO>>
                {
                    Status = false,
                    StatusCode = 400,
                    Message = "no data found",
                    Data = null
                };
            }
            return new APIResponse<List<AppointmentDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = appointmentsDTOs
            };

        }



    }
}