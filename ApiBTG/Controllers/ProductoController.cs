using ApiBTG.Application.Productos.Commands.CreateProducto;
using ApiBTG.Application.Productos.Commands.DeleteProducto;
using ApiBTG.Application.Productos.Commands.UpdateProducto;
using ApiBTG.Application.Productos.Queries.GetProductoById;
using ApiBTG.Application.Productos.Queries.GetProductos;
using ApiBTG.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBTG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IMediator mediator, ILogger<ProductoController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        // GET: api/Producto
        [HttpGet]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<ProductoDto>>>> GetProductos()
        {
            try
            {
                var query = new GetProductosQuery();
                var productos = await _mediator.Send(query);

                return Ok(ApiResponseDto<IEnumerable<ProductoDto>>.SuccessResult(productos, "Productos obtenidos exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos");
                return StatusCode(500, ApiResponseDto<IEnumerable<ProductoDto>>.ErrorResult("Error interno del servidor"));
            }
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<ProductoDto>>> GetProducto(int id)
        {
            try
            {
                var query = new GetProductoByIdQuery { Id = id };
                var producto = await _mediator.Send(query);

                if (producto == null)
                {
                    return NotFound(ApiResponseDto<ProductoDto>.ErrorResult($"Producto con ID {id} no encontrado"));
                }

                return Ok(ApiResponseDto<ProductoDto>.SuccessResult(producto, "Producto obtenido exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<ProductoDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // POST: api/Producto
        [HttpPost]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<ProductoDto>>> CreateProducto(CreateProductoDto createProductoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<ProductoDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new CreateProductoCommand
                {
                    Nombre = createProductoDto.Nombre,
                    TipoProducto = createProductoDto.TipoProducto
                };

                var producto = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id },
                    ApiResponseDto<ProductoDto>.SuccessResult(producto, "Producto creado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                return StatusCode(500, ApiResponseDto<ProductoDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<ProductoDto>>> UpdateProducto(int id, UpdateProductoDto updateProductoDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(ApiResponseDto<ProductoDto>.ErrorResult("Datos de entrada inválidos", errors));
                }

                var command = new UpdateProductoCommand
                {
                    Id = id,
                    Nombre = updateProductoDto.Nombre,
                    TipoProducto = updateProductoDto.TipoProducto
                };

                var producto = await _mediator.Send(command);

                return Ok(ApiResponseDto<ProductoDto>.SuccessResult(producto, "Producto actualizado exitosamente"));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseDto<ProductoDto>.ErrorResult(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<ProductoDto>.ErrorResult("Error interno del servidor"));
            }
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "usuario")]
        public async Task<ActionResult<ApiResponseDto<bool>>> DeleteProducto(int id)
        {
            try
            {
                var command = new DeleteProductoCommand { Id = id };
                var result = await _mediator.Send(command);

                if (!result)
                {
                    return NotFound(ApiResponseDto<bool>.ErrorResult($"Producto con ID {id} no encontrado"));
                }

                return Ok(ApiResponseDto<bool>.SuccessResult(true, "Producto eliminado exitosamente"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto con ID {Id}", id);
                return StatusCode(500, ApiResponseDto<bool>.ErrorResult("Error interno del servidor"));
            }
        }
    }
} 