using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
