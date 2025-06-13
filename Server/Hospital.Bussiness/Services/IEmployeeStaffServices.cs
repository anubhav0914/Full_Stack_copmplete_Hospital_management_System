using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IEmployeeStaffServices
    {

        Task<APIResponse<EmployeeStaffDTO>> RegisterEmployee(EmployeeStaffRequestDTO patient);
        Task<APIResponse<List<EmployeeStaffDTO>>> GetAllEmployee();

        Task<APIResponse<EmployeeStaffDTO>> GetById(int id);

        Task<APIResponse<EmployeeStaffDTO>> Update(EmployeeStaffRequestDTO patient);

        Task<APIResponse<EmployeeStaffDTO>> Delete(int id);


        Task<APIResponse<List<EmployeeStaffDTO>>> GetEmployeeByDepartmentIdAsync(int departmentId);



    }
}