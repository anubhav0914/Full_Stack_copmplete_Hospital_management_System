namespace Hospital.Bussiness.Utils
{

    public class LoginResponse

    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }

        public string JwtToken { get; set; }
        public string Message { get; set; }


    }
}