using ApiBTG.Domain.Dtos;
using MediatR;

namespace ApiBTG.Application.Productos.Queries.GetProductos
{
    public record GetProductosQuery : IRequest<IEnumerable<ProductoDto>>
    {
    }
} 