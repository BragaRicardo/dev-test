using DevTest_API.Entities.Enums;
using System.ComponentModel;

namespace DevTest_API.DTOs
{
    public class UsuarioResponseDto
    {
        [Description("Identificador único")]
        public int Id { get; set; }

        [Description("Nombre Completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        [Description("Número de Cédula")]
        public string Cedula { get; set; } = string.Empty;

        [Description("Sexo de la persona")]
        public Sexo Sexo { get; set; }

        [Description("Correo personal del usuario")]
        public string Correo { get; set; } = string.Empty;

        [Description("Dirección de residencia")]
        public string Direccion { get; set; } = string.Empty;

        [Description("Rol del usuario")]
        public Rol Rol { get; set; }

        [Description("Estado del usuario")]
        public Estado Estado { get; set; }

        [Description("Fecha de registro del usuario")]
        public DateTime FechaRegistro { get; set; }
    }
}