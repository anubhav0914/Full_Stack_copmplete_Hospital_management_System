using Hospital.Bussiness.DTOs;
using Hospital.Bussiness.Utils;
using BCrypt.Net;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Hospital.Persistence.Repository.TableRepository;
using Hospital.Bussiness.Services.AuthServices;


namespace Hospital.Bussiness.Services.AuthServices
{

    public class LoginServices : ILoginServices
    {

        private readonly IPatientRepository _patinetRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmployeeStaffRepository _employeestaffRepository;
        private readonly IAdminRepository _adminRepository;



        private readonly ITokenServices _tokenServices;


        public LoginServices(
            IPatientRepository patinetRepository,
             IDoctorRepository doctorRepository,
             IEmployeeStaffRepository employeestaffRepository,
             IAdminRepository adminRepository,
             ITokenServices tokenServices
             )
        {
            _patinetRepository = patinetRepository;
            _doctorRepository = doctorRepository;
            _employeestaffRepository = employeestaffRepository;
            _tokenServices = tokenServices;
            _adminRepository = adminRepository;
        }

        public async Task<LoginResponse> Login(LoginRequestDTO loginDTO)
        {
            var role = loginDTO.Role.ToLower();
            int id = 0;
            dynamic user = null;
            if (role == "patient")
            {
                user = await _patinetRepository.GetByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Status = false,
                        StatusCode = 404,
                        JwtToken = null,
                        Message = "Invalid email "
                    };
                }
                id = user.PatientId;
            }
            else if (role == "doctor")
            {
                user = await _doctorRepository.GetByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Status = false,
                        StatusCode = 404,
                        JwtToken = null,
                        Message = "Invalid email "
                    };
                }
                id = user.DoctorId;

            }
            else if (role == "staff")
            {
                user = await _employeestaffRepository.GetByEmailAsync(loginDTO.Email);
                if (user == null)
                {
                    return new LoginResponse
                    {
                        Status = false,
                        StatusCode = 404,
                        JwtToken = null,
                        Message = "Invalid email "
                    };
                }
                id = user.EmpId;

            }
            else
            {
                return new LoginResponse
                {
                    Status = false,
                    StatusCode = 401,
                    Message = "Invalid Role",
                    JwtToken = null,

                };
            }
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password))
                return new LoginResponse
                {
                    Status = true,
                    StatusCode = 401,
                    JwtToken = null,
                    Message = "Unauthorized access wrong password"
                }; ;

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Role, loginDTO.Role) // taking  role from request dto
                };

            var Jwttoken = _tokenServices.GenerateToken(claims);


            return new LoginResponse
            {
                Status = true,
                StatusCode = 200,
                Message = "Logged in successfully",
                JwtToken = Jwttoken,

            };

        }



        public async Task<LoginResponse> AdminLogin(AdminloginDTO loginDTO)
        {
            var admin = await _adminRepository.GetByEmailAsync(loginDTO.Email);


            if (admin == null || admin.Username != loginDTO.Username || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, admin.PasswordHash))
            {
                return new LoginResponse
                {
                    Status = false,
                    StatusCode = 401,
                    Message = "Invalid Credentials",
                    JwtToken = null,

                };
            }
            int id = admin.AdminId;
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Email, admin.Email!),
                    new Claim(ClaimTypes.Role, "admin") 

                };

            var Jwttoken = _tokenServices.GenerateToken(claims);

            return new LoginResponse
            {
                Status = true,
                StatusCode = 200,
                Message = "Logged in successfully",
                JwtToken = Jwttoken,

            };
        }

    }
}