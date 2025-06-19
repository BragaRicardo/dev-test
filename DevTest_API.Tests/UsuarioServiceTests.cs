using DevTest_API.DTOs;
using DevTest_API.Entities.Enums;
using DevTest_API.Repositories.Interfaces;
using DevTest_API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class UsuarioServiceTests
{
    [Fact]
    public async Task CrearAsync_DeberiaCrearUsuarioActivo()
    {
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<DevTest_API.Entities.Usuario>()))
                .ReturnsAsync((DevTest_API.Entities.Usuario u) => { u.Id = 1; u.Estado = Estado.ACTIVO; return u; });

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var dto = new UsuarioCreateDto
        {
            NombreCompleto = "Juan Perez",
            Correo = "juan@correo.com",
            Password = "abcd1234",
            Rol = Rol.ADMIN
        };

        var result = await service.CrearAsync(dto);

        Assert.Equal("Juan Perez", result.NombreCompleto);
        Assert.Equal(Estado.ACTIVO, result.Estado);
    }

    [Fact]
    public async Task ObtenerTodosAsync_DeberiaRetornarListaDeUsuarios()
    {
        var lista = new List<DevTest_API.Entities.Usuario>
    {
        new() { Id = 1, NombreCompleto = "A", Correo = "a@mail.com", Rol = Rol.ADMIN, Estado = Estado.ACTIVO, FechaRegistro = DateTime.UtcNow }
    };
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetAllAsync(null, null)).ReturnsAsync(lista);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var result = await service.ObtenerTodosAsync();
        Assert.Single(result);
        Assert.Equal("A", result.First().NombreCompleto);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_DeberiaRetornarUsuario()
    {
        var usuario = new DevTest_API.Entities.Usuario { Id = 1, NombreCompleto = "A", Correo = "a@mail.com", Rol = Rol.ADMIN, Estado = Estado.ACTIVO, FechaRegistro = DateTime.UtcNow };
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var result = await service.ObtenerPorIdAsync(1);
        Assert.Equal(1, result.Id);
        Assert.Equal("A", result.NombreCompleto);
    }

    [Fact]
    public async Task ObtenerPorIdAsync_UsuarioNoExiste_DeberiaRetornarNull()
    {
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DevTest_API.Entities.Usuario?)null!);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var result = await service.ObtenerPorIdAsync(99);
        Assert.Null(result);
    }

    [Fact]
    public async Task ActualizarAsync_DeberiaActualizarUsuario()
    {
        var usuario = new DevTest_API.Entities.Usuario
        {
            Id = 1,
            NombreCompleto = "Original",
            Correo = "old@mail.com",
            Rol = Rol.ADMIN,
            Estado = Estado.ACTIVO,
            FechaRegistro = DateTime.UtcNow
        };

        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(usuario);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<DevTest_API.Entities.Usuario>()))
                .ReturnsAsync((DevTest_API.Entities.Usuario u) => u);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var dto = new UsuarioCreateDto
        {
            NombreCompleto = "Nuevo",
            Correo = "nuevo@mail.com",
            Password = "12345678",
            Rol = Rol.CONSULTOR
        };

        var result = await service.ActualizarAsync(1, dto);
        Assert.Equal("Nuevo", result.NombreCompleto);
        Assert.Equal("nuevo@mail.com", result.Correo);
        Assert.Equal(Rol.CONSULTOR, result.Rol);
    }

    [Fact]
    public async Task EliminarAsync_DeberiaEliminarUsuario()
    {
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var resultado = await service.EliminarAsync(1);
        Assert.True(resultado);
    }

    [Fact]
    public async Task ValidarCredencialesAsync_LoginCorrecto_DeberiaRetornarUsuario()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("abcd1234");
        var usuario = new DevTest_API.Entities.Usuario
        {
            Id = 1,
            Correo = "user@mail.com",
            PasswordHash = hash,
            Estado = Estado.ACTIVO
        };
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetByCorreoAsync("user@mail.com")).ReturnsAsync(usuario);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var result = await service.ValidarCredencialesAsync("user@mail.com", "abcd1234");
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task ValidarCredencialesAsync_LoginIncorrecto_DeberiaRetornarNull()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("abcd1234");
        var usuario = new DevTest_API.Entities.Usuario
        {
            Id = 1,
            Correo = "user@mail.com",
            PasswordHash = hash,
            Estado = Estado.ACTIVO
        };
        var mockRepo = new Mock<IUsuarioRepository>();
        mockRepo.Setup(r => r.GetByCorreoAsync("user@mail.com")).ReturnsAsync(usuario);

        var logger = Mock.Of<ILogger<UsuarioService>>();
        var service = new UsuarioService(mockRepo.Object, logger);

        var result = await service.ValidarCredencialesAsync("user@mail.com", "malapass");
        Assert.Null(result);
    }
}
