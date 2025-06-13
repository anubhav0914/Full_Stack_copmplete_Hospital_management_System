using Hospital.Persistence.Models;
using Hospital.Bussiness.Utils;
using Hospital.Bussiness.DTOs;
using System;

namespace Hospital.Bussiness.Services
{

    public interface IBillingTransicationServices
    {

        public Task<APIResponse<BillingTransactionDTO>> AddNewBill(BillingTransactionRequestDTO bill);
       
        public Task<APIResponse<List<BillingTransactionDTO>>> GetAllBills();

        public Task<APIResponse<BillingTransactionDTO>> GetById(int id);

        public Task<APIResponse<List<BillingTransactionDTO>>> GetBillByDoctorID(int id);
        public Task<APIResponse<List<BillingTransactionDTO>>> GetBillByPatientID(int id);
        public Task<APIResponse<List<BillingTransactionDTO>>> GetBillByDate(DateTime date);

    }
}