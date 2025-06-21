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
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository repo, ILogger<UsuarioService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ObtenerTodosAsync(string? nombre = null, Estado? estado = null)
        {
            var usuarios = await _repo.GetAllAsync(nombre, estado);
            _logger.LogInformation("Listado de usuarios: {Cantidad}", usuarios.Count());
            return usuarios.Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Cedula = u.Cedula,
                Sexo = u.Sexo,
                Correo = u.Correo,
                Direccion = u.Direccion,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            });
        }

        public async Task<UsuarioResponseDto> ObtenerPorIdAsync(int id)
        {
            var u = await _repo.GetByIdAsync(id);
            if (u == null)
            {
                _logger.LogWarning("Usuario no encontrado. ID: {id}", id);
                return null;
            }
            _logger.LogInformation("Usuario encontrado: {Usuario}", u.NombreCompleto);
            return new UsuarioResponseDto
            {
                Id = u.Id,
                NombreCompleto = u.NombreCompleto,
                Cedula = u.Cedula,
                Sexo = u.Sexo,
                Correo = u.Correo,
                Direccion = u.Direccion,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            };
        }

        public async Task<UsuarioResponseDto> CrearAsync(UsuarioCreateDto dto)
        {
            _logger.LogInformation("Intentando crear usuario: {Correo}", dto.Correo);
            var nuevo = new Usuario
            {
                NombreCompleto = dto.NombreCompleto,
                Cedula = dto.Cedula,
                Sexo = dto.Sexo,
                Correo = dto.Correo,
                Direccion = dto.Direccion,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12),
                Rol = dto.Rol,
                Estado = Estado.ACTIVO
            };
            var creado = await _repo.CreateAsync(nuevo);
            _logger.LogInformation("Usuario creado correctamente: {Id}", creado.Id);
            return new UsuarioResponseDto
            {
                Id = creado.Id,
                NombreCompleto = creado.NombreCompleto,
                Cedula = creado.Cedula,
                Sexo = creado.Sexo,
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
            if (existente == null)
            {
                _logger.LogWarning("Intento de actualizar usuario inexistente. ID: {id}", id);
                throw new KeyNotFoundException($"Usuario con ID {id} no encontrado.");
            }
            existente.NombreCompleto = dto.NombreCompleto;
            existente.Cedula = dto.Cedula;
            existente.Sexo = dto.Sexo;
            existente.Correo = dto.Correo;
            existente.Direccion = dto.Direccion;
            existente.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password, workFactor: 12);
            existente.Rol = dto.Rol;
            var actualizado = await _repo.UpdateAsync(existente);
            _logger.LogInformation("Usuario actualizado correctamente: {Id}", actualizado.Id);
            return new UsuarioResponseDto
            {
                Id = actualizado.Id,
                NombreCompleto = actualizado.NombreCompleto,
                Cedula = actualizado.Cedula,
                Sexo = actualizado.Sexo,
                Correo = actualizado.Correo,
                Direccion = actualizado.Direccion,
                Rol = actualizado.Rol,
                Estado = actualizado.Estado,
                FechaRegistro = actualizado.FechaRegistro
            };
        }

        public async Task<bool> EliminarAsync(int id)
        {
            _logger.LogInformation("Intentando eliminar usuario. ID: {id}", id);
            var resultado = await _repo.DeleteAsync(id);
            if (resultado)
                _logger.LogInformation("Usuario eliminado correctamente. ID: {id}", id);
            else
                _logger.LogWarning("No se pudo eliminar usuario porque no existe. ID: {id}", id);
            return resultado;
        }

        public async Task<Usuario?> ValidarCredencialesAsync(string correo, string password)
        {
            _logger.LogInformation("Intentando login para usuario: {Correo}", correo);
            var user = await _repo.GetByCorreoAsync(correo);
            if (user == null)
            {
                _logger.LogWarning("Usuario no encontrado para correo: {Correo}", correo);
                return null;
            }
            if (user.Estado != Estado.ACTIVO)
            {
                _logger.LogWarning("Usuario inactivo. ID: {id}", user.Id);
                return null;
            }
            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                _logger.LogInformation("Login exitoso para usuario: {Correo}", correo);
                return user;
            }
            _logger.LogWarning("Login fallido para usuario: {Correo}", correo);
            return null;
        }
    }
}