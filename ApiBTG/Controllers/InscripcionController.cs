using ApiBTG.Application.Inscripciones.Commands.CreateInscripcion;
using ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion;
using ApiBTG.Application.Inscripciones.Commands.UpdateInscripcion;
using ApiBTG.Application.Inscripciones.Queries.GetInscripcionById;
using ApiBTG.Application.Inscripciones.Queries.GetInscripciones;
using ApiBTG.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InscripcionController> _logger;

        public InscripcionController(IMediator mediator, ILogger<InscripcionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Inscripcion
        [HttpGet]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<InscripcionDto>>>> GetInscripciones()
        {
            try
            {
                var query = new GetInscripcionesQuery();
                var inscripciones = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<InscripcionDto>>.SuccessResult(inscripciones, "Inscripciones obtenidas exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener inscripciones");
                return StatusCode(500, ApiResponseDto<IEnumerable<InscripcionDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Inscripcion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDto<InscripcionDto>>> GetInscripcion(int id)
        {
            try
            {
                var query = new GetInscripcionByIdQuery 
                { 
                    Id = id
                };
                var inscripcion = await _mediator.Send(query);

                if (inscripcion == null)
                {
                    return NotFound(ApiResponseDto<InscripcionDto>.ErrorResult($"Inscripción con ID {id} no encontrada"));
                }

                return Ok(ApiResponseDto<InscripcionDto>.SuccessResult(inscripcion, "Inscripción obtenida exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener inscripción con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<InscripcionDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/Inscripcion
        [HttpPost]
        public async Task<ActionResult<ApiResponseDto<InscripcionDto>>> CreateInscripcion(CreateInscripcionDto createInscripcionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<InscripcionDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateInscripcionCommand
                {
                    IdCliente = createInscripcionDto.IdCliente,
                    IdDisponibilidad = createInscripcionDto.IdDisponibilidad
                };

                var inscripcion = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetInscripcion), 
                    new { id = inscripcion.Id },
                    ApiResponseDto<InscripcionSimpleDto>.SuccessResult(inscripcion, "Inscripción creada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<InscripcionDto>.ErrorResult(ex.Message));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponseDto<InscripcionDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear inscripción");
                return StatusCode(500, ApiResponseDto<InscripcionDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Inscripcion/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseDto<InscripcionDto>>> UpdateInscripcion(int id, UpdateInscripcionDto updateInscripcionDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<InscripcionDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateInscripcionCommand
                {
                    Id = id,
                    IdCliente = updateInscripcionDto.IdCliente,
                    IdDisponibilidad = updateInscripcionDto.IdDisponibilidad
                };

                var inscripcion = await _mediator.Send(command);

                return Ok(ApiResponseDto<InscripcionDto>.SuccessResult(inscripcion, "Inscripción actualizada exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<InscripcionDto>.ErrorResult(ex.Message));
            }
            //catch (ValidationException ex)
            //{
            //    var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
            //    return BadRequest(ApiResponseDto<InscripcionDto>.ErrorResult("Error de validación", errors));
            //}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar inscripción con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<InscripcionDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Inscripcion/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteInscripcion(int id)
        {
            try
            {
                var command = new DeleteInscripcionCommand 
                { 
                    Id = id
                };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Inscripción con ID {id} no encontrada"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Inscripción eliminada exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar inscripción con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 