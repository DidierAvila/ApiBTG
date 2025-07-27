using ApiBTG.Application.Disponibilidades.Commands.CreateDisponibilidad;
using ApiBTG.Application.Disponibilidades.Commands.DeleteDisponibilidad;
using ApiBTG.Application.Disponibilidades.Commands.UpdateDisponibilidad;
using ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidadById;
using ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidades;
using ApiBTG.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisponibilidadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DisponibilidadController> _logger;

        public DisponibilidadController(IMediator mediator, ILogger<DisponibilidadController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Disponibilidad
        [HttpGet]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<DisponibilidadDto>>>> GetDisponibilidades()
        {
            try
            {
                var query = new GetDisponibilidadesQuery();
                var disponibilidades = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<DisponibilidadDto>>.SuccessResult(disponibilidades, "Disponibilidades obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener disponibilidades");
                return StatusCode(500, ApiResponseDto<IEnumerable<DisponibilidadDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Disponibilidad/5/10
        [HttpGet("{idSucursal}/{idProducto}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<DisponibilidadDto>>> GetDisponibilidad(int idSucursal, int idProducto)
        {
            try
            {
                var query = new GetDisponibilidadByIdQuery 
                { 
                    IdSucursal = idSucursal, 
                    IdProducto = idProducto 
                };
                var disponibilidad = await _mediator.Send(query);

                if (disponibilidad == null)
                {
                    return NotFound(ApiResponseDto<DisponibilidadDto>.ErrorResult($"Disponibilidad con Sucursal ID {idSucursal} y Producto ID {idProducto} no encontrada"));
                }

                return Ok(ApiResponseDto<DisponibilidadDto>.SuccessResult(disponibilidad, "Disponibilidad obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener disponibilidad con Sucursal ID {IdSucursal} y Producto ID {IdProducto}", idSucursal, idProducto);
                return StatusCode(500, ApiResponseDto<DisponibilidadDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/Disponibilidad
        [HttpPost]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<DisponibilidadDto>>> CreateDisponibilidad(CreateDisponibilidadDto createDisponibilidadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<DisponibilidadDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateDisponibilidadCommand
                {
                    IdSucursal = createDisponibilidadDto.IdSucursal,
                    IdProducto = createDisponibilidadDto.IdProducto,
                    MontoMinimo = createDisponibilidadDto.MontoMinimo
                };

                var disponibilidad = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetDisponibilidad), 
                    new { idSucursal = disponibilidad.IdSucursal, idProducto = disponibilidad.IdProducto },
                    ApiResponseDto<DisponibilidadDto>.SuccessResult(disponibilidad, "Disponibilidad creada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<DisponibilidadDto>.ErrorResult(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponseDto<DisponibilidadDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear disponibilidad");
                return StatusCode(500, ApiResponseDto<DisponibilidadDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Disponibilidad/5/10
        [HttpPut("{idSucursal}/{idProducto}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<DisponibilidadDto>>> UpdateDisponibilidad(int idSucursal, int idProducto, UpdateDisponibilidadDto updateDisponibilidadDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<DisponibilidadDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateDisponibilidadCommand
                {
                    IdSucursal = idSucursal,
                    IdProducto = idProducto,
                    NewIdSucursal = updateDisponibilidadDto.IdSucursal,
                    NewIdProducto = updateDisponibilidadDto.IdProducto,
                    MontoMinimo = updateDisponibilidadDto.MontoMinimo
                };

                var disponibilidad = await _mediator.Send(command);

                return Ok(ApiResponseDto<DisponibilidadDto>.SuccessResult(disponibilidad, "Disponibilidad actualizada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<DisponibilidadDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar disponibilidad con Sucursal ID {IdSucursal} y Producto ID {IdProducto}", idSucursal, idProducto);
                return StatusCode(500, ApiResponseDto<DisponibilidadDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Disponibilidad/5/10
        [HttpDelete("{idSucursal}/{idProducto}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteDisponibilidad(int idSucursal, int idProducto)
        {
            try
            {
                var command = new DeleteDisponibilidadCommand 
                { 
                    IdSucursal = idSucursal, 
                    IdProducto = idProducto 
                };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Disponibilidad con Sucursal ID {idSucursal} y Producto ID {idProducto} no encontrada"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Disponibilidad eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar disponibilidad con Sucursal ID {IdSucursal} y Producto ID {IdProducto}", idSucursal, idProducto);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 