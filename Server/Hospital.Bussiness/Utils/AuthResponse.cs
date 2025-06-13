namespace Hospital.Bussiness.DTOs
{


    public class AuthResponse<T>
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public DateTime ExpiresAt { get; set; }
        T Data { get; set; }
    }

}