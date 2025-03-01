using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class AuthRegister
    {
        [EmailAddress]
        [MinLength(3)]
        [MaxLength(100)]
        public required string Email { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(50)]
        public required string Password1 { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(50)]
        public required string Password2 { get; set; } = string.Empty;
    }
}
