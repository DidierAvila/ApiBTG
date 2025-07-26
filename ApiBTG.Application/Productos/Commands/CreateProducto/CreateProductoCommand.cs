using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Productos.Commands.CreateProducto
{
    public record CreateProductoCommand : IRequest<ProductoDto>
    {
        public string Nombre { get; init; } = string.Empty;
        public string TipoProducto { get; init; } = string.Empty;
    }
} 