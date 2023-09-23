namespace Web_Api_LPasto_ASP_NET_Core.Models
{
    public class ResponseService<T>
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
