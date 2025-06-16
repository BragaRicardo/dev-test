using DevTest_API.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevTest_API.DTOs
{
    public class UsuarioCreateDto
    {
        [Required, MaxLength(100)]
        [Description("Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required, MaxLength(8)]
        [Description("Número de Cédula")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [Description("Sexo de la persona")]
        public Sexo Sexo { get; set; }

        [Required, EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [Description("Correo personal del usuario")]
        public string Correo { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        [Description("Dirección de residencia")]
        public string Direccion { get; set; } = string.Empty;

        [Required, StringLength(64, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 64 caracteres.")]
        [Description("Contraseña en texto plano (será hasheada)")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Description("Rol del usuario")]
        public Rol Rol { get; set; }
    }
}