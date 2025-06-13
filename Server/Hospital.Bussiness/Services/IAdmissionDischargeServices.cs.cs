using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IAdmissionDischargeServices
    {
        public Task<APIResponse<List<AdmissionDischargeDTO>>> GetAllAdmissionDischarge();
    }
}