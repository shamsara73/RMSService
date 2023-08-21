namespace RMSServices.Models
{
    public class AuthResponseWrapper
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
