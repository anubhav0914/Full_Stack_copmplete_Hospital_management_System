using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using Hospital.Persistence.Repository.TableRepository;

using System;

namespace Hospital.Bussiness.Services
{

    public class AdmissionDischargeServices :IAdmissionDischargeServices
    {
        private readonly IAdmissionDischargeRepository _admissionDischargeRepository;

        public AdmissionDischargeServices(IAdmissionDischargeRepository admissionDischargeRepository)
        {
            _admissionDischargeRepository = admissionDischargeRepository;
        }
        public async  Task<APIResponse<List<AdmissionDischargeDTO>>> GetAllAdmissionDischarge()
        {
            var admissionDischarge = await _admissionDischargeRepository.GetAllAsync();

            if (admissionDischarge == null || !admissionDischarge.Any())
            {
                return new APIResponse<List<AdmissionDischargeDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var admissionDischargeDTOs = admissionDischarge.Select(p => new AdmissionDischargeDTO
            {

                AdmitId = p.AdmitId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                DischargeDate = p.DischargeDate,
                AdmissionDate = p.AdmissionDate
                
            }).ToList();

            return new APIResponse<List<AdmissionDischargeDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = admissionDischargeDTOs
            };

        }
    }
}