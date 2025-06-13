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


namespace Hospital.Bussiness.Services
{

    public class PatientServices : IPatientServices
    {

        private readonly IPatientRepository _patientRepository;
        private readonly ITokenServices _tokenServices;
        private readonly IMailService _mailService;

        public PatientServices(IPatientRepository patinetRepository, ITokenServices tokenServices, IMailService mailService)
        {
            _patientRepository = patinetRepository;
            _tokenServices = tokenServices;
            _mailService = mailService;
        }

        public async Task<APIResponse<PatientDTO>> Register(PatientRequestDTO reqModel)
        {
            try
            {
                var existingPatient = await _patientRepository.GetByEmailAsync(reqModel.Email);

                if (existingPatient != null)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = false,
                        Message = "Patient already exists with this emial ",
                        Data = null
                    };
                }

                var patient = new Patient
                {
                    FirstName = reqModel.FirstName,
                    LastName = reqModel.LastName,
                    Gender = reqModel.Gender,
                    AddressLine1 = reqModel.AddressLine1,
                    AddressLine2 = reqModel.AddressLine2,
                    AdmissionDate = DateTime.UtcNow,
                    PhoneNumber = reqModel.PhoneNumber,
                    Email = reqModel.Email,
                    BloodGroup = reqModel.BloodGroup,
                    Password = BCrypt.Net.BCrypt.HashPassword(reqModel.Password),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };

                var added = await _patientRepository.AddAsync(patient);

                if (!added)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = false,
                        StatusCode = 400,
                        Message = "Something went wrong while adding patient",
                        Data = null
                    };
                }

                await _mailService.SendEmailPatientAsync(reqModel.Email, reqModel.FirstName+" "+reqModel.LastName);
                return new APIResponse<PatientDTO>
                {
                    Status = true,
                    StatusCode = 200,
                    Message = "Successfully registered",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<PatientDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = ex.ToString(),
                    Data = null
                };
            }
        }



        public async Task<APIResponse<List<PatientDTO>>> GetAllPatient()
        {
            var patients = await _patientRepository.GetAllAsync();


            if (patients == null || !patients.Any())
            {
                return new APIResponse<List<PatientDTO>>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No data found",
                    Data = null
                };
            }

            var patientDTOs = patients.Select(p => new PatientDTO
            {
                PatientId = p.PatientId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = p.Gender,
                AddressLine1 = p.AddressLine1,
                AddressLine2 = p.AddressLine2,
                AdmissionDate = p.AdmissionDate,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                BloodGroup = p.BloodGroup,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate
            }).ToList();

            return new APIResponse<List<PatientDTO>>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = patientDTOs
            };
        }

        public async Task<APIResponse<PatientDTO>> GetById(int id)
        {
            var patient = await _patientRepository.GetById(id);

            if (patient == null)
            {
                return new APIResponse<PatientDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Patient with this Id",
                    Data = null
                };
            }

            var patientDTOs = new PatientDTO
            {

                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                AddressLine1 = patient.AddressLine1,
                AddressLine2 = patient.AddressLine2,
                AdmissionDate = patient.AdmissionDate,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                BloodGroup = patient.BloodGroup,
                CreatedDate = patient.CreatedDate,
                UpdatedDate = patient.UpdatedDate
            };

            return new APIResponse<PatientDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = patientDTOs
            };
        }

        public async Task<APIResponse<PatientDTO>> Update(PatientRequestDTO patient)
        {
            try
            {
                var existingPatient = await _patientRepository.GetById(patient.PatientId);

                if (existingPatient == null)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "Patient not found",
                        Data = null
                    };
                }

                existingPatient.FirstName = patient.FirstName;
                existingPatient.LastName = patient.LastName;
                existingPatient.Gender = patient.Gender;
                existingPatient.AddressLine1 = patient.AddressLine1;
                existingPatient.AddressLine2 = patient.AddressLine2;
                existingPatient.AdmissionDate = patient.AdmissionDate;
                existingPatient.PhoneNumber = patient.PhoneNumber;
                existingPatient.Email = patient.Email;
                existingPatient.BloodGroup = patient.BloodGroup;
                existingPatient.UpdatedDate = DateTime.UtcNow;

                var added = await _patientRepository.UpdateAsync(existingPatient);

                if (added)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Updated successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<PatientDTO>
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
                return new APIResponse<PatientDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<APIResponse<PatientDTO>> Delete(int id)
        {
            try
            {
                var existingPatient = await _patientRepository.GetById(id);

                if (existingPatient == null)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = false,
                        StatusCode = 404,
                        Message = "No data found with the given ID",
                        Data = null
                    };
                }

                var result = await _patientRepository.DeleteAsync(id);

                if (result)
                {
                    return new APIResponse<PatientDTO>
                    {
                        Status = true,
                        StatusCode = 200,
                        Message = "Data deleted successfully",
                        Data = null
                    };
                }
                else
                {
                    return new APIResponse<PatientDTO>
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
                return new APIResponse<PatientDTO>
                {
                    Status = false,
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }


        public async Task<APIResponse<PatientDTO>> GetByEmail(string emial)
        {
          var patient = await _patientRepository.GetByEmailAsync(emial);

            if (patient == null)
            {
                return new APIResponse<PatientDTO>
                {
                    Status = false,
                    StatusCode = 404,
                    Message = "No Patient with this email",
                    Data = null
                };
            }

            var patientDTOs = new PatientDTO
            {

                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Gender = patient.Gender,
                AddressLine1 = patient.AddressLine1,
                AddressLine2 = patient.AddressLine2,
                AdmissionDate = patient.AdmissionDate,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                BloodGroup = patient.BloodGroup,
                CreatedDate = patient.CreatedDate,
                UpdatedDate = patient.UpdatedDate
            };

            return new APIResponse<PatientDTO>
            {
                Status = true,
                StatusCode = 200,
                Message = "Data fetched",
                Data = patientDTOs
            };   
        }


    //     public async Task<LoginResponse> Login(LoginRequestDTO loginDTO)
        //     {

        //         var patient = await _patientRepository.GetByEmailAsync(loginDTO.Email);
        //         if (patient == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, patient.Password))
        //             return new LoginResponse
        //         {
        //             Status = true,
        //             StatusCode = 401,
        //             JwtToken = null,
        //             Message = "unauthorized access"

        //         };;

        //         var claims = new List<Claim>
        //             {
        //                 new Claim(ClaimTypes.NameIdentifier, patient.PatientId.ToString()),
        //                 new Claim(ClaimTypes.Email, patient.Email!),
        //                 new Claim(ClaimTypes.Role, loginDTO.Role) // role from request
        //             };

        //         var Jwttoken = _tokenServices.GenerateToken(claims);



        //         return new LoginResponse
        //         {
        //             Status = true,
        //             StatusCode = 200,
        //             Message = "Logged in successfully",
        //             JwtToken = Jwttoken,

        //         };

        // }

    }
}