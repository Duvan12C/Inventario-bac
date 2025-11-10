using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; } = string.Empty;
    }
}
