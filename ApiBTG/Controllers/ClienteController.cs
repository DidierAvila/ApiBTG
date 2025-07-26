using ApiBTG.Application.Clientes.Commands.CreateCliente;
using ApiBTG.Application.Clientes.Commands.DeleteCliente;
using ApiBTG.Application.Clientes.Commands.UpdateCliente;
using ApiBTG.Application.Clientes.Queries.GetClienteById;
using ApiBTG.Application.Clientes.Queries.GetClientes;
using ApiBTG.Application.Clientes.Queries.GetClientesConInscripcionesEnSucursalesVisitadas;
using ApiBTG.Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IMediator mediator, ILogger<ClienteController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Cliente
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ClienteDto>>>> GetClientes()
        {
            try
            {
                var query = new GetClientesQuery();
                var clientes = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<ClienteDto>>.SuccessResult(clientes, "Clientes obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener clientes");
                return StatusCode(500, ApiResponseDto<IEnumerable<ClienteDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Cliente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<ClienteDto>>> GetCliente(int id)
        {
            try
            {
                var query = new GetClienteByIdQuery { Id = id };
                var cliente = await _mediator.Send(query);

                if (cliente == null)
                {
                    return NotFound(ApiResponseDto<ClienteDto>.ErrorResult($"Cliente con ID {id} no encontrado"));
                }

                return Ok(ApiResponseDto<ClienteDto>.SuccessResult(cliente, "Cliente obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener cliente con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<ClienteDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Cliente/con-inscripciones-en-sucursales-visitadas
        [HttpGet("sucursales-visitadas")]
        public async Task<IActionResult> GetClientesConInscripcionesEnSucursalesVisitadas(
            [FromQuery] int? clienteId = null,
            [FromQuery] int? sucursalId = null)
        {
            var query = new GetClientesConInscripcionesEnSucursalesVisitadasQuery
            {
                ClienteId = clienteId,
                SucursalId = sucursalId
            };

            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        // POST: api/Cliente
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<ClienteDto>>> CreateCliente(CreateClienteDto createClienteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<ClienteDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateClienteCommand
                {
                    Nombre = createClienteDto.Nombre,
                    Apellidos = createClienteDto.Apellidos,
                    Ciudad = createClienteDto.Ciudad
                };

                var cliente = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id },
                    ApiResponseDto<ClienteDto>.SuccessResult(cliente, "Cliente creado exitosamente"));
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponseDto<ClienteDto>.ErrorResult("Error de validación", errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cliente");
                return StatusCode(500, ApiResponseDto<ClienteDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Cliente/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<ClienteDto>>> UpdateCliente(int id, UpdateClienteDto updateClienteDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<ClienteDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateClienteCommand
                {
                    Id = id,
                    Nombre = updateClienteDto.Nombre,
                    Apellidos = updateClienteDto.Apellidos,
                    Ciudad = updateClienteDto.Ciudad
                };

                var cliente = await _mediator.Send(command);

                return Ok(ApiResponseDto<ClienteDto>.SuccessResult(cliente, "Cliente actualizado exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<ClienteDto>.ErrorResult(ex.Message));
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponseDto<ClienteDto>.ErrorResult("Error de validación", errors));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar cliente con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<ClienteDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteCliente(int id)
        {
            try
            {
                var command = new DeleteClienteCommand { Id = id };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Cliente con ID {id} no encontrado"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Cliente eliminado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar cliente con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 