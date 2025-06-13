using System.Security.Claims;

namespace Hospital.Bussiness.Services.AuthServices
{
    public interface ITokenServices
    {
        string GenerateToken(List<Claim> claims);

    }
}