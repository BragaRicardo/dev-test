using DevTest_API.Configuration;
using DevTest_API.DTOs;
using DevTest_API.Entities;
using DevTest_API.Entities.Enums;
using DevTest_API.Repositories.Interfaces;
using DevTest_API.Services.Interfaces;

namespace DevTest_API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        public UsuarioService(IUsuarioRepository repo) => _repo = repo;

        public async Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosAsync(string nombre = null, Estado? estado = null)
        {
            var usuarios = await _repo.GetAllAsync(nombre, estado);
            return usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Correo = u.Correo,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            });
        }

        public async Task<UsuarioResponseDto?> ObtenerPorIdAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null) return null;
            return new UsuarioResponseDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Correo = u.Correo,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            };
        }

        public async Task<UsuarioResponseDto> CrearAsync(UsuarioCreateDto dto)
        {
            var nuevo = new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Cedula = dto.Cedula,
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                PasswordHash = dto.Password,
                Rol = dto.Rol,
                Estado = Estado.ACTIVO
            };
            var creado = await _repo.CreateAsync(nuevo);
            return new UsuarioResponseDto
            {
                Id = creado.Id,
                NombreCompleto = creado.NombreCompleto,
                Cedula = creado.Cedula,
                Correo = creado.Correo,
                Direccion = creado.Direccion,
                Rol = creado.Rol,
                Estado = creado.Estado,
                FechaRegistro = creado.FechaRegistro
            };
        }

        public async Task<UsuarioResponseDto> ActualizarAsync(int id, UsuarioCreateDto dto)
        {
            var existente = await _repo.GetByIdAsync(id);
            if (existente == null) return null;
            existente.NombreCompleto = dto.NombreCompleto;
            existente.Cedula = dto.Cedula;
            existente.Correo = dto.Correo;
            existente.Direccion = dto.Direccion;
            existente.PasswordHash = dto.Password;
            existente.Rol = dto.Rol;
            var actualizado = await _repo.UpdateAsync(existente);
            return new UsuarioResponseDto
            {
                Id = actualizado.Id,
                NombreCompleto = actualizado.NombreCompleto,
                Cedula = actualizado.Cedula,
                Correo = actualizado.Correo,
                Direccion = actualizado.Direccion,
                Rol = actualizado.Rol,
                Estado = actualizado.Estado,
                FechaRegistro = actualizado.FechaRegistro
            };
        }

        public async Task<bool> EliminarAsync(int id) => await _repo.DeleteAsync(id);

        public async Task<string?> LoginAsync(string correo, string contrasena)
        {
            var usuarios = await _repo.GetAllAsync(correo, Estado.ACTIVO);
            var user = usuarios.FirstOrDefault(u => u.Correo == correo && u.PasswordHash == contrasena);
            if (user == null) return null;
            return "token_generado_simulado";
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string password)
        {
            var u = await _repo.GetByCorreoAsync(correo);
            if (u == null || u.Estado != Estado.ACTIVO)
                return null;
            var decodedPassword = Base64Utils.Decode(u.PasswordHash);
            return decodedPassword == password ? u : null;
        }
    }
}
