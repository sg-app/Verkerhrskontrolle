namespace Verkehrskontrolle.Models
{
    public class AuthenticateResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
