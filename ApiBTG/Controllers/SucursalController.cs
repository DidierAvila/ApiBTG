using ApiBTG.Application.Sucursales.Commands.CreateSucursal;
using ApiBTG.Application.Sucursales.Commands.DeleteSucursal;
using ApiBTG.Application.Sucursales.Commands.UpdateSucursal;
using ApiBTG.Application.Sucursales.Queries.GetSucursalById;
using ApiBTG.Application.Sucursales.Queries.GetSucursales;
using ApiBTG.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SucursalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SucursalController> _logger;

        public SucursalController(IMediator mediator, ILogger<SucursalController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Sucursal
        [HttpGet]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<SucursalDto>>>> GetSucursales()
        {
            try
            {
                var query = new GetSucursalesQuery();
                var sucursales = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<SucursalDto>>.SuccessResult(sucursales, "Sucursales obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sucursales");
                return StatusCode(500, ApiResponseDto<IEnumerable<SucursalDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Sucursal/5
        [HttpGet("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<SucursalDto>>> GetSucursal(int id)
        {
            try
            {
                var query = new GetSucursalByIdQuery { Id = id };
                var sucursal = await _mediator.Send(query);

                if (sucursal == null)
                {
                    return NotFound(ApiResponseDto<SucursalDto>.ErrorResult($"Sucursal con ID {id} no encontrada"));
                }

                return Ok(ApiResponseDto<SucursalDto>.SuccessResult(sucursal, "Sucursal obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener sucursal con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<SucursalDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/Sucursal
        [HttpPost]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<SucursalDto>>> CreateSucursal(CreateSucursalDto createSucursalDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<SucursalDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateSucursalCommand
                {
                    Nombre = createSucursalDto.Nombre,
                    Ciudad = createSucursalDto.Ciudad
                };

                var sucursal = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetSucursal), new { id = sucursal.Id },
                    ApiResponseDto<SucursalDto>.SuccessResult(sucursal, "Sucursal creada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear sucursal");
                return StatusCode(500, ApiResponseDto<SucursalDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Sucursal/5
        [HttpPut("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<SucursalDto>>> UpdateSucursal(int id, UpdateSucursalDto updateSucursalDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<SucursalDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateSucursalCommand
                {
                    Id = id,
                    Nombre = updateSucursalDto.Nombre,
                    Ciudad = updateSucursalDto.Ciudad
                };

                var sucursal = await _mediator.Send(command);

                return Ok(ApiResponseDto<SucursalDto>.SuccessResult(sucursal, "Sucursal actualizada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<SucursalDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar sucursal con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<SucursalDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Sucursal/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteSucursal(int id)
        {
            try
            {
                var command = new DeleteSucursalCommand { Id = id };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Sucursal con ID {id} no encontrada"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Sucursal eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar sucursal con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 