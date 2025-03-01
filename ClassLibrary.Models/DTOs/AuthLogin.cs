using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class AuthLogin
    {
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
