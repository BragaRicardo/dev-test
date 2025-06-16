using DevTest_API.Entities.Enums;
using System.ComponentModel;

namespace DevTest_API.DTOs
{
    public class UsuarioResponseDto
    {
        [Description("Identificador único")]
        public int Id { get; set; }

        [Description("Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Description("Número de Cédula")]
        public string Cedula { get; set; }

        [Description("Sexo de la persona")]
        public Sexo Sexo { get; set; }

        [Description("Correo personal del usuario")]
        public string Correo { get; set; }

        [Description("Dirección de residencia")]
        public string Direccion { get; set; }

        [Description("Clave/Contraseña Cifrada")]
        public string Password { get; set; }

        [Description("Rol del usuario")]
        public Rol Rol { get; set; }

        [Description("Estado del usuario")]
        public Estado Estado { get; set; }

        [Description("Fecha de registro del usuario")]
        public DateTime FechaRegistro { get; set; }
    }
}