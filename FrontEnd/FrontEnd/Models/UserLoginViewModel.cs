using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class UserLoginViewModel
    {
        [Display(Name = "Correo")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Contraseña")]
        public string Password { get; set; } = string.Empty;


    }
}
