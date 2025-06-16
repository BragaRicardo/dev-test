using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevTest_API.DTOs
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        [Description("Correo personal del usuario")]
        public string Correo { get; set; }
        [Required]
        [Description("Clave/Contraseña Cifrada")]
        public string Password { get; set; }
    }
}