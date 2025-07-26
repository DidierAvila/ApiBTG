using ApiBTG.Application.Visitas.Commands.CreateVisita;
using ApiBTG.Application.Visitas.Commands.DeleteVisita;
using ApiBTG.Application.Visitas.Commands.UpdateVisita;
using ApiBTG.Application.Visitas.Queries.GetVisitaById;
using ApiBTG.Application.Visitas.Queries.GetVisitas;
using ApiBTG.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VisitaController> _logger;

        public VisitaController(IMediator mediator, ILogger<VisitaController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Visita
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<VisitaDto>>>> GetVisitas()
        {
            try
            {
                var query = new GetVisitasQuery();
                var visitas = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<VisitaDto>>.SuccessResult(visitas, "Visitas obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener visitas");
                return StatusCode(500, ApiResponseDto<IEnumerable<VisitaDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Visita/5/10
        [HttpGet("{idSucursal}/{idCliente}")]
        public async Task<ActionResult<ApiResponseDto<VisitaDto>>> GetVisita(int idSucursal, int idCliente)
        {
            try
            {
                var query = new GetVisitaByIdQuery 
                { 
                    IdSucursal = idSucursal, 
                    IdCliente = idCliente 
                };
                var visita = await _mediator.Send(query);

                if (visita == null)
                {
                    return NotFound(ApiResponseDto<VisitaDto>.ErrorResult($"Visita con Sucursal ID {idSucursal} y Cliente ID {idCliente} no encontrada"));
                }

                return Ok(ApiResponseDto<VisitaDto>.SuccessResult(visita, "Visita obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener visita con Sucursal ID {IdSucursal} y Cliente ID {IdCliente}", idSucursal, idCliente);
                return StatusCode(500, ApiResponseDto<VisitaDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/Visita
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<VisitaDto>>> CreateVisita(CreateVisitanDto createVisitanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<VisitaDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateVisitaCommand
                {
                    IdSucursal = createVisitanDto.IdSucursal,
                    IdCliente = createVisitanDto.IdCliente,
                    FechaVisita = createVisitanDto.FechaVisita
                };

                var visita = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetVisita), 
                    new { idSucursal = visita.IdSucursal, idCliente = visita.IdCliente },
                    ApiResponseDto<VisitaDto>.SuccessResult(visita, "Visita creada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<VisitaDto>.ErrorResult(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponseDto<VisitaDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear visita");
                return StatusCode(500, ApiResponseDto<VisitaDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Visita/5/10
        [HttpPut("{idSucursal}/{idCliente}")]
        public async Task<ActionResult<ApiResponseDto<VisitaDto>>> UpdateVisita(int idSucursal, int idCliente, UpdateVisitanDto updateVisitanDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<VisitaDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateVisitaCommand
                {
                    IdSucursal = idSucursal,
                    IdCliente = idCliente,
                    NewIdSucursal = updateVisitanDto.IdSucursal,
                    NewIdCliente = updateVisitanDto.IdCliente,
                    FechaVisita = updateVisitanDto.FechaVisita
                };

                var visita = await _mediator.Send(command);

                return Ok(ApiResponseDto<VisitaDto>.SuccessResult(visita, "Visita actualizada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<VisitaDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar visita con Sucursal ID {IdSucursal} y Cliente ID {IdCliente}", idSucursal, idCliente);
                return StatusCode(500, ApiResponseDto<VisitaDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Visita/5/10
        [HttpDelete("{idSucursal}/{idCliente}")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteVisita(int idSucursal, int idCliente)
        {
            try
            {
                var command = new DeleteVisitaCommand 
                { 
                    IdSucursal = idSucursal, 
                    IdCliente = idCliente 
                };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Visita con Sucursal ID {idSucursal} y Cliente ID {idCliente} no encontrada"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Visita eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar visita con Sucursal ID {IdSucursal} y Cliente ID {IdCliente}", idSucursal, idCliente);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 