using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Productos.Commands.DeleteProducto
{
    public class DeleteProductoCommandHandler : IRequestHandler<DeleteProductoCommand, bool>
    {
        private readonly IProductoRepository _productoRepository;

        public DeleteProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<bool> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _productoRepository.Delete(request.Id, cancellationToken);
            return deletedEntity != null;
        }
    }
} 