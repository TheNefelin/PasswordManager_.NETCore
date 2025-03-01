namespace ClassLibrary.Models.DTOs
{
    public class UserDTO
    {
        public string IdUser { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string HashLogin { get; set; } = string.Empty;
        public string SaltLogin { get; set; } = string.Empty;
        public string HashPM { get; set; } = string.Empty;
        public string SaltPM { get; set; } = string.Empty;
        public string SqlToken { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
