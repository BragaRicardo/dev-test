using DevTest_API.DTOs;
using DevTest_API.Entities;
using DevTest_API.Entities.Enums;
using DevTest_API.Services.Interfaces;
using DevTest_API.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevTest_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<Usuario> _logger;

        public UsuariosController(IUsuarioService usuarioService, ILogger<Usuario> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        /// <summary>
        /// Método para listar usuarios con filtros opcionales.
        /// </summary>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="estado">Estado</param>
        /// <returns></returns>
        /// <response code="200">Solicitud Realizada Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="401">No autorizado. Token ausente o inválido</response>
        /// <response code="500">Problema Interno de Aplicación</response>
        [HttpGet]
        [Authorize(Policy = "AdminOConsultor")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> ListarUsuarios([FromQuery] string? nombre, [FromQuery] Estado? estado)
        {
            try
            {
                _logger.LogInformation("Validando si existen datos");
                var resultado = await _usuarioService.ObtenerTodosAsync(nombre, estado);

                if (!resultado.Any())
                {
                    _logger.LogInformation("No hay datos");
                    return NoContent();
                }

                _logger.LogInformation("Usuarios encontrados: {@resultado}", resultado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un error al intentar obtener el Listado de usuarios");
                return Problem(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Método que obtiene el listado de un usuario específico.
        /// </summary>
        /// <param name="id">Identificador Único del usuario</param>
        /// <returns></returns>
        /// <response code="200">Registro Encontrado</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="401">No autorizado. Token ausente o inválido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminOConsultor")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> BuscarUsuario(int id)
        {

            try
            {
                _logger.LogInformation("Buscando usuario con ID: {id}", id);
                var resultado = await _usuarioService.ObtenerPorIdAsync(id);

                if (resultado == null)
                {
                    _logger.LogInformation("No se encontró el usuario con ID: {id}", id);
                    return NoContent();
                }

                _logger.LogInformation("Usuario Encontrado: {@resultado}", resultado);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener el usuario con ID: {id}", id);
                return Problem(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Método para insertar un nuevo usuario.
        /// </summary>
        /// <param name="usuarioDto">Datos del usuario en formato JSON</param>
        /// <returns></returns>
        /// <response code="201">Insertado Correctamente</response>
        /// <response code="400">Solicitud Incorrecta</response>
        /// <response code="401">No autorizado. Token ausente o inválido</response>
        /// <response code="500">Problema Interno de Aplicación</response>
        [HttpPost]
        [Authorize(Policy = "SoloAdmin")]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 500)]
        [HttpPost]
        public async Task<IActionResult> InsertarUsuario([FromBody] UsuarioCreateDto usuarioDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validando datos de entrada");

                var validator = new UsuarioValidator();
                var result = await validator.ValidateAsync(usuarioDto, cancellationToken);

                if (!result.IsValid)
                {
                    _logger.LogWarning("Errores de validación: {@Errores}", result.Errors);
                    return BadRequest(result.Errors.Select(e => new { campo = e.PropertyName, mensaje = e.ErrorMessage }));
                }

                var nuevoUsuario = await _usuarioService.CrearAsync(usuarioDto);
                _logger.LogInformation("Usuario insertado correctamente {@Usuario}", nuevoUsuario);

                return CreatedAtAction(nameof(BuscarUsuario), new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al intentar registrar el usuario");
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Método que actualiza los datos de un usuario.
        /// </summary>
        /// <param name="id">Identificador Único del usuario</param>
        /// <param name="usuarioDto">Datos del usuario en formato JSON</param>
        /// <returns></returns>
        /// <response code="200">Actualizado Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="401">No autorizado. Token ausente o inválido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpPut("{id}")]
        [Authorize(Policy = "SoloAdmin")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] UsuarioCreateDto usuarioDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Validando datos de entrada para actualización");
                var validator = new UsuarioValidator();
                var validationResult = await validator.ValidateAsync(usuarioDto, cancellationToken);

                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Errores de validación: {@Errores}", validationResult.Errors);
                    return BadRequest(validationResult.Errors.Select(e => new
                    {
                        campo = e.PropertyName,
                        mensaje = e.ErrorMessage
                    }));
                }

                _logger.LogInformation("Buscando usuario con ID: {id}", id);
                var resultado = await _usuarioService.ObtenerPorIdAsync(id);

                if (resultado == null)
                {
                    _logger.LogInformation("No se encontró el usuario con ID: {id}", id);
                    return NoContent();
                }

                var usuarioActualizado = await _usuarioService.ActualizarAsync(id, usuarioDto);
                _logger.LogInformation("Usuario actualizado correctamente {@Usuario}", usuarioActualizado);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un error al intentar actualizar el usuario");
                return Problem(ex.Message);
            }
        }

        /// <summary>
        /// Método que elimina un usuario por su ID.
        /// </summary>
        /// <param name="id">Identificador único del usuario</param>
        /// <response code="200">Eliminado Correctamente</response>
        /// <response code="204">Sin Contenido</response>
        /// <response code="401">No autorizado. Token ausente o inválido</response>
        /// <response code="500">Problema interno de aplicación</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "SoloAdmin")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 401)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                _logger.LogInformation("Buscando usuario con ID: {id}", id);
                var resultado = await _usuarioService.ObtenerPorIdAsync(id);

                if (resultado == null)
                {
                    _logger.LogInformation("No se encontró el usuario con ID: {id}", id);
                    return NoContent();
                }

                await _usuarioService.EliminarAsync(id);

                _logger.LogInformation("Usuario eliminado correctamente. ID: {id}", id);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrio un error al intentar eliminar el usuario");
                return Problem(ex.Message);
            }
        }
    }
}
