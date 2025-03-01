namespace ClassLibrary.Models.DTOs
{
    public class AuthLogged
    {
        public string IdUser { get; set; } = string.Empty;
        public string SqlToken { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ExpireMin { get; set; } = string.Empty;
        public string ApiToken { get; set; } = string.Empty;
    }
}
