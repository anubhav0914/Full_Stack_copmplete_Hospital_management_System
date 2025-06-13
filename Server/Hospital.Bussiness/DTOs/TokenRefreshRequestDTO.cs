namespace Hospital.Bussiness.DTOs
{

    public class TokenRefreshRequestDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}