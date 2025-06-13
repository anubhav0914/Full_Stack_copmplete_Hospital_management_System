using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IDepartmentServices
    {

        public Task<APIResponse<DepartmentDTO>> AddDepartment(DepartmentRequestDTO department);
        public Task<APIResponse<List<DepartmentDTO>>> GetAllDepartment();

        public  Task<APIResponse<DepartmentDTO>> GetById(int id);

       public  Task<APIResponse<DepartmentDTO>> Update(DepartmentRequestDTO department);

        public Task<APIResponse<DepartmentDTO>> Delete(int id);


    }
}