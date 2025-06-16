using DevTest_API.Configuration;
using DevTest_API.DTOs;
using DevTest_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevTest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioService usuarioService,
                              ITokenService tokenService,
                              IOptions<JwtSettings> opts,
                              ILogger<AuthController> logger)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
            _jwtSettings = opts.Value;
            _logger = logger;
        }

        /// <summary>
        /// Autentica un usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="dto">Credenciales del usuario (correo y contraseña)</param>
        /// <returns>Token JWT y tiempo de expiración</returns>
        /// <response code="200">Autenticación exitosa</response>
        /// <response code="401">Credenciales inválidas o usuario inactivo</response>
        /// <response code="500">Error interno del servidor</response>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                var usuario = await _usuarioService.ValidarCredencialesAsync(dto.Correo, dto.Password);
                if (usuario == null)
                    return Unauthorized(new { mensaje = "Credenciales inválidas o usuario inactivo." });

                var token = _tokenService.GenerarToken(usuario);
                return Ok(new LoginResponseDto
                {
                    Token = token,
                    ExpiraEn = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar autenticar al usuario con correo: {Correo}", dto.Correo);
                return StatusCode(500, new { mensaje = "Ocurrió un error inesperado al intentar autenticar." });
            }
        }
    }
}
