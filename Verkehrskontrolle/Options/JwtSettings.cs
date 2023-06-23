namespace Verkehrskontrolle.Options
{
    public class JwtSettings
    {
        public static string SectionName = "JwtSettings";
        public string Secret { get; set; } = "VerkehrskontrolleSecret";
        public string Issuer { get; set; } = "Verkehrskontrolle API";
        public string Audience { get; set; } = "Verkehrskontrolle API";
        public int ExpireMinutes { get; set; } = 60;

    }
}
