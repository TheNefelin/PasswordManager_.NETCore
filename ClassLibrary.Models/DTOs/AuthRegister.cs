using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class AuthRegister
    {
        [EmailAddress]
        [MinLength(3)]
        [MaxLength(100)]
        [Required]
        public string Email { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(50)]
        [Required]
        public string Password1 { get; set; } = string.Empty;

        [MinLength(6)]
        [MaxLength(50)]
        [Required]
        public string Password2 { get; set; } = string.Empty;
    }
}
