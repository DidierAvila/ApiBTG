using MediatR;

namespace ApiBTG.Application.Productos.Commands.DeleteProducto
{
    public record DeleteProductoCommand : IRequest<bool>
    {
        public int Id { get; init; }
    }
} 