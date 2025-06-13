using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;
using Hospital.Persistence.Repository.TableRepository;

namespace Hospital.Bussiness.Services
{

    public class BillingTransicationServices : IBillingTransicationServices
    {

        private readonly IBillingTrasicationRepository _billingTransicationRepository;
        private readonly IAdmissionDischargeRepository _admissionDischargeRepository;
        private readonly IPatientRepository _patientRepository;

        public BillingTransicationServices(IBillingTrasicationRepository billingTransicationRepository,
                                 IAdmissionDischargeRepository admissionDischargeRepository,
                                 IPatientRepository patientRepository
                                 )
        {
            _billingTransicationRepository = billingTransicationRepository;
            _admissionDischargeRepository = admissionDischargeRepository;
            _patientRepository = patientRepository;
        }
        public async Task<APIResponse<BillingTransactionDTO>> AddNewBill(BillingTransactionRequestDTO bill)
        {
            int admisionAdmitid = 0;
            var billDTO = new BillingTransaction
            {
                PatientId = bill.PatientId,
                AppointmentId = bill.AppointmentId,
                TotalAmount = bill.TotalAmount,
                BillingDate = DateTime.UtcNow,
                DoctorId = bill.DoctorId
            };

            if (bill.DoctorId!=null && bill.PatientId!= null)
            {

                var patient = await _patientRepository.GetById(bill.PatientId);
                Random rnd = new Random();
                var admissionDischargeDTO = new AdmissionDischarge
                {
                    DoctorId = bill.DoctorId,
                    PatientId = bill.PatientId,
                    AdmissionDate = patient.AdmissionDate,
                    DischargeDate = DateTime.UtcNow,
                    RoomNo = rnd.Next(1, 50)
                };

                var added = await _admissionDischargeRepository.AddAsync(admissionDischargeDTO);
                if (!added)
                {
                    return new APIResponse<BillingTransactionDTO>
                    {
                        Status = false,
                        StatusCode = 500,
                        Message = "Something went wrong while adding addmission discharge data",
                        Data = null
                    };
                }
            }


            var result = await _billingTransicationRepository.AddAsync(billDTO);

            if (!result)
            {
                return new APIResponse<BillingTransactionDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = "Something went wrong while adding billing data",
                    Data = null
                };
            }
            return new APIResponse<BillingTransactionDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Added succesfully",
                Data = null
            };
        }

        public async Task<APIResponse<List<BillingTransactionDTO>>> GetAllBills()
        {
            var bills = await _billingTransicationRepository.GetAllAsync();

            if (bills == null || !bills.Any())
            {
                return new APIResponse<List<BillingTransactionDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var billDTOs = bills.Select(p => new BillingTransactionDTO
            {
                BillId = p.BillId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                BillingDate = p.BillingDate,
                TotalAmount = p.TotalAmount,
                AppointmentId = p.AppointmentId,
               

            }).ToList();

            return new APIResponse<List<BillingTransactionDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = billDTOs
            };

        }

        public async Task<APIResponse<BillingTransactionDTO>> GetById(int id)
        {
            var bill = await _billingTransicationRepository.GetById(id);
            if (bill == null)
            {
                return new APIResponse<BillingTransactionDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Appoinment found with this Id",
                    Data = null
                };
            }

                var billDTO = new BillingTransactionDTO
                {   
                    BillId = bill.BillId,
                    PatientId = bill.PatientId,
                    AppointmentId = bill.AppointmentId,
                    TotalAmount = bill.TotalAmount,
                    BillingDate = DateTime.UtcNow,
                    DoctorId = bill.DoctorId
                };


            return new APIResponse<BillingTransactionDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = billDTO
            };
        }

        public async Task<APIResponse<List<BillingTransactionDTO>>> GetBillByDoctorID(int id)
        {
            var bills = await _billingTransicationRepository.GetBillByDoctorIDAsync(id);

            if (bills == null)
            {
                return new APIResponse<List<BillingTransactionDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var billDTOs = bills.Select(p => new BillingTransactionDTO
            {
                BillId = p.BillId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                BillingDate = p.BillingDate,
                TotalAmount = p.TotalAmount,
                AppointmentId = p.AppointmentId
               

            }).ToList();

            return new APIResponse<List<BillingTransactionDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = billDTOs
            };


        }
        public async Task<APIResponse<List<BillingTransactionDTO>>> GetBillByPatientID(int id)
        {
           var bills = await _billingTransicationRepository.GetBillByPatientIDAsync(id);

            if (bills == null)
            {
                return new APIResponse<List<BillingTransactionDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var billDTOs = bills.Select(p => new BillingTransactionDTO
            {
                 BillId = p.BillId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                BillingDate = p.BillingDate,
                TotalAmount = p.TotalAmount,
                AppointmentId = p.AppointmentId
               

            }).ToList();

            return new APIResponse<List<BillingTransactionDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = billDTOs
            };
        }
        public async Task<APIResponse<List<BillingTransactionDTO>>> GetBillByDate(DateTime date)
        {
          var bills = await _billingTransicationRepository.GetBillsyDateAsync(date);

            if (bills == null)
            {
                return new APIResponse<List<BillingTransactionDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var billDTOs = bills.Select(p => new BillingTransactionDTO
            {
                 BillId = p.BillId,
                DoctorId = p.DoctorId,
                PatientId = p.PatientId,
                BillingDate = p.BillingDate,
                TotalAmount = p.TotalAmount,
                AppointmentId = p.AppointmentId
               
            }).ToList();

            return new APIResponse<List<BillingTransactionDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = billDTOs
            };
        }

    }
}