using Hospital.Bussiness.Utils;
using Hospital.Persistence.Repository.TableRepository;
using Hospital.Persistence.Models;
using Hospital.Bussiness.DTOs;
using Hospital.Bussiness.Services.AuthServices;
using BCrypt.Net;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;


namespace Hospital.Bussiness.Services
{

    public class DoctorServices : IDoctorServices
    {

        private readonly IDoctorRepository _doctorRepository;
        private readonly IDepartmentRepository _departmentRepository;


        public DoctorServices(IDoctorRepository doctorRepository, IDepartmentRepository departmentRepository)
        {
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task<APIResponse<DoctorDTO>> RegisterDoctor(DoctorRequestDTO reqModel)
        {
            try
            {
                var existingDoctor = await _doctorRepository.GetByEmailAsync(reqModel.Email);

                if (existingDoctor != null)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = false,
                        Message = "Doctor already exists with this emial ",
                        Data = null
                    };
                }

                var doctor = new Doctor
                {
                    FirstName = reqModel.FirstName,
                    LastName = reqModel.LastName,
                    Specialization = reqModel.Specialization,
                    PhoneNumber = reqModel.PhoneNumber,
                    Email = reqModel.Email,
                    Qualification = reqModel.Qualification,
                    ExperienceYear = reqModel.ExperienceYear,
                    JoiningDate = reqModel.JoiningDate,
                    Availability = reqModel.Availability,
                    DepartmentId = reqModel.DepartmentId,
                    Password = BCrypt.Net.BCrypt.HashPassword(reqModel.Password)
                };

                var added = await _doctorRepository.AddAsync(doctor);

                if (!added)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Something went wrong while adding patient",
                        Data = null
                    };
                }
                if (reqModel.DepartmentId.HasValue)
                {
                    var department = await _departmentRepository.GetById(reqModel.DepartmentId.Value);
                    if (department != null)
                    {
                        department.NoOfEmployees += 1;
                        await _departmentRepository.UpdateAsync(department);
                    }
                }
                return new APIResponse<DoctorDTO>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Successfully registered",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<DoctorDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.ToString(),
                    Data = null
                };
            }
        }



        public async Task<APIResponse<List<DoctorDTO>>> GetAllDoctors()
        {
            var doctors = await _doctorRepository.GetAllAsync();

            if (doctors == null || !doctors.Any())
            {
                return new APIResponse<List<DoctorDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var doctorDTOs = doctors.Select(p => new DoctorDTO
            {
                DoctorId = p.DoctorId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Specialization = p.Specialization,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Qualification = p.Qualification,
                ExperienceYear = p.ExperienceYear,
                JoiningDate = p.JoiningDate,
                Availability = p.Availability,
                DepartmentId = p.DepartmentId

            }).ToList();

            return new APIResponse<List<DoctorDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = doctorDTOs
            };
        }

        public async Task<APIResponse<DoctorDTO>> GetById(int id)
        {
            var doctor = await _doctorRepository.GetById(id);

            if (doctor == null)
            {
                return new APIResponse<DoctorDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Patient with this Id",
                    Data = null
                };
            }

            var doctorDTOs = new DoctorDTO
            {

                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email,
                Qualification = doctor.Qualification,
                ExperienceYear = doctor.ExperienceYear,
                JoiningDate = doctor.JoiningDate,
                Availability = doctor.Availability,
                DepartmentId = doctor.DepartmentId
            };

            return new APIResponse<DoctorDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = doctorDTOs
            };
        }

        public async Task<APIResponse<DoctorDTO>> Update(DoctorRequestDTO doctor)
        {
            try
            {
                var existingDoctor = await _doctorRepository.GetById(doctor.DoctorId);

                if (existingDoctor == null)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Patient not found",
                        Data = null
                    };
                }

                existingDoctor.FirstName = doctor.FirstName;
                existingDoctor.LastName = doctor.LastName;
                existingDoctor.Specialization = doctor.Specialization;
                existingDoctor.PhoneNumber = doctor.PhoneNumber;
                existingDoctor.Email = doctor.Email;
                existingDoctor.Qualification = doctor.Qualification;
                existingDoctor.ExperienceYear = doctor.ExperienceYear;
                existingDoctor.JoiningDate = doctor.JoiningDate;
                existingDoctor.Availability = doctor.Availability;
                existingDoctor.DepartmentId = doctor.DepartmentId;


                var added = await _doctorRepository.UpdateAsync(existingDoctor);

                if (added)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Updated successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = false,
                        StatusCode = 500,
                        Message = "Something went wrong while updating",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new APIResponse<DoctorDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<DoctorDTO>> Delete(int id)
        {
            try
            {
                var existingDoctor = await _doctorRepository.GetById(id);

                if (existingDoctor == null)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No data found with the given ID",
                        Data = null
                    };
                }

                var result = await _doctorRepository.DeleteAsync(id);

                if (result)
                {
                    return new APIResponse<DoctorDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Data deleted successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<DoctorDTO>
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
                return new APIResponse<DoctorDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<List<DayOfWeek>>> GetAvaibillity(int id)
        {
            var doctor = await _doctorRepository.GetById(id);

            if (doctor == null)
            {
                return new APIResponse<List<DayOfWeek>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Patient with this Id",
                    Data = null
                };
            }

            return new APIResponse<List<DayOfWeek>>
            {
                Status = true,
                StatusCode = 404,
                Message = "No Patient with this Id",
                Data = doctor.Availability
            };
        }


        public async Task<APIResponse<List<DoctorDTO>>> GetDoctorsByDepartmentIdAsync(int departmentId)
        {
            var doctors = await _doctorRepository.GetDoctorsByDepartmentIdAsync(departmentId);

            if (doctors == null)
            {
                return new APIResponse<List<DoctorDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No doctor with this  deprtment Id",
                    Data = null
                };
            }

            var doctorDTOs = doctors.Select(p => new DoctorDTO
            {
                DoctorId = p.DoctorId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Specialization = p.Specialization,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Qualification = p.Qualification,
                ExperienceYear = p.ExperienceYear,
                JoiningDate = p.JoiningDate,
                Availability = p.Availability,
                DepartmentId = p.DepartmentId

            }).ToList();

            return new APIResponse<List<DoctorDTO>>
            {
                Status = false,
                StatusCode = 500,
                Message = "succesfully fetched data",
                Data = doctorDTOs
            };
        }

    }
}