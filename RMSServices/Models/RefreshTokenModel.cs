namespace RMSServices.Models
{
    public class RefreshTokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public int UserID { get; set; }
    }
}
