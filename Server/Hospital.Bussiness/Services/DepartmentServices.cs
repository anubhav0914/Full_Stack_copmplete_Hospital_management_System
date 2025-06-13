using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Repository.TableRepository;
using System;

namespace Hospital.Bussiness.Services
{

    public class DepartmentServices : IDepartmentServices
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmployeeStaffRepository _employeestaffRepository;


        public DepartmentServices(
            IDepartmentRepository departmentRepossitory,
             IDoctorRepository doctorRepository,
             IEmployeeStaffRepository employeestaffRepository
             )
        {
            _departmentRepository = departmentRepossitory;
            _doctorRepository = doctorRepository;
            _employeestaffRepository = employeestaffRepository;

        }


        public async Task<APIResponse<DepartmentDTO>> AddDepartment(DepartmentRequestDTO department)
        {

            try
            {
                var existingDepartment = await _departmentRepository.GetByNameAsync(department.DepartmentName);

                if (existingDepartment != null)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = false,
                        Message = "Department already exists with this Name ",
                        Data = null
                    };
                }
                var doctourCount = await _doctorRepository.GetDoctorsByDepartmentIdAsync(department.DepartmentId);
                int doctCount = doctourCount.Count();
                var EmployeeCount = await _employeestaffRepository.GetEmployeeByDepartmentIdAsync(department.DepartmentId);
                int empCount = EmployeeCount.Count();
                int total = doctCount + empCount;
                var departmentDto = new Department
                {
                    DepartmentName = department.DepartmentName,
                    DepartmentHead = department.DepartmentHead,
                    CreationDate = DateTime.UtcNow,
                    NoOfEmployees = total

                };

                var added = await _departmentRepository.AddAsync(departmentDto);

                if (!added)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Something went wrong while adding patient",
                        Data = null
                    };
                }

                return new APIResponse<DepartmentDTO>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Successfully registered",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<DepartmentDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.ToString(),
                    Data = null
                };
            }
        }
        public async Task<APIResponse<List<DepartmentDTO>>> GetAllDepartment()
        {
            var department = await _departmentRepository.GetAllAsync();

            if (department == null || !department.Any())
            {
                return new APIResponse<List<DepartmentDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var departmentDTOs = department.Select(p => new DepartmentDTO
            {
                DepartmentId = p.DepartmentId,
                DepartmentName = p.DepartmentName,
                DepartmentHead = p.DepartmentHead,
                CreationDate = p.CreationDate,
                NoOfEmployees = p.NoOfEmployees

            }).ToList();

            return new APIResponse<List<DepartmentDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = departmentDTOs
            };
        }


       public async  Task<APIResponse<DepartmentDTO>> GetById(int id)
        {
            var department = await _departmentRepository.GetById(id);

            if (department == null)
            {
                return new APIResponse<DepartmentDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No department with this Id",
                    Data = null
                };
            }
            var departmentDTO = new DepartmentDTO
            {

                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                DepartmentHead = department.DepartmentHead,
                CreationDate = department.CreationDate,
                NoOfEmployees = department.NoOfEmployees
            };

            return new APIResponse<DepartmentDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = departmentDTO
            };
        }

        public async  Task<APIResponse<DepartmentDTO>> Update(DepartmentRequestDTO department)
        {
            try
            {
                var existingDepartment = await _departmentRepository.GetById(department.DepartmentId);

                if (existingDepartment == null)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Patient not found",
                        Data = null
                    };
                }

                existingDepartment.DepartmentName = department.DepartmentName;
                existingDepartment.DepartmentHead = department.DepartmentHead;


                var added = await _departmentRepository.UpdateAsync(existingDepartment);

                if (added)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Updated successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<DepartmentDTO>
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
                return new APIResponse<DepartmentDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<DepartmentDTO>> Delete(int id)
        {
        try
            {
                var existingDepartment = await _departmentRepository.GetById(id);

                if (existingDepartment == null)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No data found with the given ID",
                        Data = null
                    };
                }

                var result = await _departmentRepository.DeleteAsync(id);

                if (result)
                {
                    return new APIResponse<DepartmentDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Data deleted successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<DepartmentDTO>
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
                return new APIResponse<DepartmentDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


    }
}