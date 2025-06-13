namespace Hospital.Bussiness.Utils
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; }

        public bool Status { get; set; }

        public string Message { get; set; } 

        public T Data { get; set; }
    }
}