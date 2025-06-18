using System;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Bussiness.Services.OTPServices;
using Hospital.Persistence;
using Hospital.Persistence.Models;
using Hospital.Persistence.Repository.TableRepository;
using Hpospital.Bussiness.Services.MailServices;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Business.Services
{
    public class OTPService : IOTPService
    {
        private readonly IOTPRepository _context;
        private readonly IMailService _mailServices;

        public OTPService(IOTPRepository context, IMailService mailService)
        {
            _context = context;
            _mailServices = mailService;
        }

        public async Task<string> GenerateOtpAsync(string email,string name)
        {
            string otp = new Random().Next(100000, 999999).ToString();

            var otpEntry = new OTPModel
            {
                Email = email,
                Otp = otp,
                ExpiryTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            
            var added =  await _context.AddAsync(otpEntry);
            if (!added)
            {
                return "";
            }

            await _mailServices.SendOTPtoAdminAsync(email, name, otp);

            return otp;
        }

        public async Task<bool> VerifyOtpAsync(string email, string otp)
        {
            var entry = await _context.GetByEmail(email, otp);
            if (entry == null) return false;
            entry.IsUsed = true;
            return true;
        }
    }
}
