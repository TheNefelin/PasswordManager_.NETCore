namespace ClassLibrary.Models.DTOs
{
    public class JwtConfig
    {
        public string Key { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string ExpireMin { get; set; } = string.Empty;
    }
}
