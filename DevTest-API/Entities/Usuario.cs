using DevTest_API.Entities.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DevTest_API.Entities
{
    public class Usuario
    {
        [Key]
        [Description("Identificador único")]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [Description("Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required, MaxLength(8)]
        [Description("Número de Cédula")]
        public string Cedula { get; set; }

        [Required]
        [Description("Sexo de la persona")]
        public Sexo Sexo { get; set; }

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

        [Required]
        [Description("Estado del usuario")]
        public Estado Estado { get; set; }

        [Description("Fecha de registro del usuario")]
        public DateTime FechaRegistro { get; set; }
    }
}