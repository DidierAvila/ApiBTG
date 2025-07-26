using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Productos.Queries.GetProductoById
{
    public record GetProductoByIdQuery : IRequest<ProductoDto?>
    {
        public int Id { get; init; }
    }
} 