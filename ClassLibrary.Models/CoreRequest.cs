using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Models
{
    public class CoreRequest<T>
    {
        [Required]
        public string IdUser { get; set; } = string.Empty;
        [Required]
        public string SqlToken { get; set; }
        public string Password { get; set; } = string.Empty;
        public T? CoreData { get; set; } // Solo para operaciones de inserción/actualización
    }
}
