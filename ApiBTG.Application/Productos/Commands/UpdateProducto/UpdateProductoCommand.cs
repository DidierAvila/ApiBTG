using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Productos.Commands.UpdateProducto
{
    public record UpdateProductoCommand : IRequest<ProductoDto>
    {
        public int Id { get; init; }
        public string Nombre { get; init; } = string.Empty;
        public string TipoProducto { get; init; } = string.Empty;
    }
} 