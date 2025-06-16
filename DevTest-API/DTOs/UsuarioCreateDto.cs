using DevTest_API.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevTest_API.DTOs
{
    public class UsuarioCreateDto
    {
        [Required, MaxLength(100)]
        [Description("Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required, MaxLength(8)]
        [Description("Número de Cédula")]
        public string Cedula { get; set; }

        [Required]
        [Description("Sexo de la persona")]
        public string Sexo { get; set; }

        [Required, EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Description("Correo personal del usuario")]
        public string Correo { get; set; }

        [Required, MaxLength(150)]
        [Description("Dirección de residencia")]
        public string Direccion { get; set; }

        [Required, StringLength(64, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 64 caracteres.")]
        [Description("Clave/Contraseña Cifrada")]
        public string Password { get; set; }

        [Required]
        [Description("Rol del usuario")]
        public Rol Rol { get; set; }
    }
}