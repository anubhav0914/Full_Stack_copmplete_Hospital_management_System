using Hospital.Bussiness.Utils;
using Hospital.Persistence.Repository.TableRepository;
using Hospital.Persistence.Models;
using Hospital.Bussiness.DTOs;
using Hospital.Bussiness.Services.AuthServices;
using BCrypt.Net;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

using System.Security.Claims;
using Hpospital.Bussiness.Services.MailServices;
using Hospital.Business.Services.ImageService;


namespace Hospital.Bussiness.Services
{

    public class EmployeeStaffServices : IEmployeeStaffServices
    {

        private readonly IEmployeeStaffRepository _employeeStaffRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMailService _mailService;
        
        private readonly IImageService _imageServices;


        public EmployeeStaffServices(IEmployeeStaffRepository employeeStaffRepository,
        IDepartmentRepository departmentRepository,
        IMailService mailService,
        IImageService imageService)
        {
            _employeeStaffRepository = employeeStaffRepository;
            _departmentRepository = departmentRepository;
            _mailService = mailService;
            _imageServices = imageService;

        }

        public async Task<APIResponse<EmployeeStaffDTO>> RegisterEmployee(EmployeeStaffRequestDTO reqModel)
        {
            try
            {
                var existingEmployee = await _employeeStaffRepository.GetByEmailAsync(reqModel.Email);

                if (existingEmployee != null)
                {
                    return new APIResponse<EmployeeStaffDTO>
                    {
                        Status = false,
                        Message = "Employee already exists with this emial ",
                        Data = null
                    };
                }

                UploadImageRequest img = new UploadImageRequest { File = reqModel.Image };

                var imgUrl = await _imageServices.UploadImageAsync(img);

                var employeestaff = new EmployeeStaff
                {
                    FirstName = reqModel.FirstName,
                    LastName = reqModel.LastName,
                    Gender = reqModel.Gender,
                    PhoneNumber = reqModel.PhoneNumber,
                    Email = reqModel.Email,
                    Role = reqModel.Role,
                    Salary = reqModel.Salary,
                    JoiningDate = reqModel.JoiningDate,
                    ProfileImage = imgUrl,
                    DepartmentId = reqModel.DepartmentId,
                    Password = BCrypt.Net.BCrypt.HashPassword(reqModel.Password)

                };

                var added = await _employeeStaffRepository.AddAsync(employeestaff);

                if (!added)
                {
                    return new APIResponse<EmployeeStaffDTO>
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
                 var emailed = await _mailService.SendEmailEmployeeAsync(reqModel.Email,reqModel.Password,reqModel.FirstName+" "+reqModel.LastName,reqModel.Role);

                return new APIResponse<EmployeeStaffDTO>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Successfully registered",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<EmployeeStaffDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.ToString(),
                    Data = null
                };
            }
        }



        public async Task<APIResponse<List<EmployeeStaffDTO>>> GetAllEmployee()
        {
            var employeestaff = await _employeeStaffRepository.GetAllAsync();

            if (employeestaff == null || !employeestaff.Any())
            {
                return new APIResponse<List<EmployeeStaffDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var employeestaffDTOs = employeestaff.Select(p => new EmployeeStaffDTO
            {
                EmpId = p.EmpId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                JoiningDate = p.JoiningDate,
                DepartmentId = p.DepartmentId,
                Gender = p.Gender,
                Role = p.Role,
                Salary = p.Salary,
                ProfileImage = p.ProfileImage

            }).ToList();

            return new APIResponse<List<EmployeeStaffDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = employeestaffDTOs
            };
        }

        public async Task<APIResponse<EmployeeStaffDTO>> GetById(int id)
        {
            var employee = await _employeeStaffRepository.GetById(id);

            if (employee == null)
            {
                return new APIResponse<EmployeeStaffDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Employee with this Id",
                    Data = null
                };
            }

            var employeeDTOs = new EmployeeStaffDTO
            {

                EmpId = employee.EmpId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                DepartmentId = employee.DepartmentId,
                Gender = employee.Gender,
                Role = employee.Role,
                Salary = employee.Salary,
                ProfileImage = employee.ProfileImage
            };

            return new APIResponse<EmployeeStaffDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = employeeDTOs
            };
        }

        public async Task<APIResponse<EmployeeStaffDTO>> Update(EmployeeStaffRequestDTO employee)
        {
            try
            {
                var existingEployee = await _employeeStaffRepository.GetById(employee.EmpId);

                if (existingEployee == null)
                {
                    return new APIResponse<EmployeeStaffDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Emplyee not found",
                        Data = null
                    };
                }
                existingEployee.EmpId = employee.EmpId;
                existingEployee.FirstName = employee.FirstName;
                existingEployee.LastName = employee.LastName;
                existingEployee.PhoneNumber = employee.PhoneNumber;
                existingEployee.Email = employee.Email;
                existingEployee.JoiningDate = employee.JoiningDate;
                existingEployee.DepartmentId = employee.DepartmentId;
                existingEployee.Gender = employee.Gender;
                existingEployee.Role = employee.Role;
                existingEployee.Salary = employee.Salary;


                var added = await _employeeStaffRepository.UpdateAsync(existingEployee);

                if (added)
                {
                    return new APIResponse<EmployeeStaffDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Updated successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<EmployeeStaffDTO>
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
                return new APIResponse<EmployeeStaffDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<EmployeeStaffDTO>> Delete(int id)
        {
            try
            {
                var existingEmployee = await _employeeStaffRepository.GetById(id);

                if (existingEmployee == null)
                {
                    return new APIResponse<EmployeeStaffDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No data found with the given ID",
                        Data = null
                    };
                }

                var result = await _employeeStaffRepository.DeleteAsync(id);

                if (result)
                {
                    return new APIResponse<EmployeeStaffDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Data deleted successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<EmployeeStaffDTO>
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
                return new APIResponse<EmployeeStaffDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<APIResponse<List<EmployeeStaffDTO>>> GetEmployeeByDepartmentIdAsync(int departmentId)
        {
            var employee = await _employeeStaffRepository.GetEmployeeByDepartmentIdAsync(departmentId);

            if (employee == null)
            {
                return new APIResponse<List<EmployeeStaffDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No doctor with this  deprtment Id",
                    Data = null
                };
            }

            var employeestaffDTOs = employee.Select(p => new EmployeeStaffDTO
            {

                EmpId = p.EmpId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                JoiningDate = p.JoiningDate,
                DepartmentId = p.DepartmentId,
                Gender = p.Gender,
                Role = p.Role,
                Salary = p.Salary,
                ProfileImage = p.ProfileImage

            }).ToList();

            return new APIResponse<List<EmployeeStaffDTO>>
            {
                Status = false,
                StatusCode = 500,
                Message = "succesfully fetched data",
                Data = employeestaffDTOs
            };
        }

    }
}