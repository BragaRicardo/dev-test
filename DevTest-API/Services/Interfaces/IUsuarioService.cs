using DevTest_API.DTOs;
using DevTest_API.Entities;
using DevTest_API.Entities.Enums;

namespace DevTest_API.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosAsync(string? nombre = null, Estado? estado = null);
        Task<UsuarioResponseDto> ObtenerPorIdAsync(int id);
        Task<UsuarioResponseDto> CrearAsync(UsuarioCreateDto dto);
        Task<UsuarioResponseDto> ActualizarAsync(int id, UsuarioCreateDto dto);
        Task<bool> EliminarAsync(int id);
        Task<Usuario?> ValidarCredencialesAsync(string correo, string password);
    }
}
