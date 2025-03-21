﻿using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models.DTOs
{
    public class CoreDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string Data01 { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        public string Data02 { get; set; } = string.Empty;
        [Required]
        [MaxLength(256)]
        public string Data03 { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string IdUser { get; set; } = string.Empty;
    }
}
