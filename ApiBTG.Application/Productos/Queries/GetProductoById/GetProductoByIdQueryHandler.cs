using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Productos.Queries.GetProductoById
{
    public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, ProductoDto?>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductoByIdQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetByID(request.Id, cancellationToken);

            if (producto == null)
                return null;

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                TipoProducto = producto.TipoProducto
            };
        }
    }
} 