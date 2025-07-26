using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Productos.Queries.GetProductos
{
    public class GetProductosQueryHandler : IRequestHandler<GetProductosQuery, IEnumerable<ProductoDto>>
    {
        private readonly IProductoRepository _productoRepository;

        public GetProductosQueryHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDto>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
        {
            var productos = await _productoRepository.GetAll(cancellationToken);

            return productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                TipoProducto = p.TipoProducto
            });
        }
    }
} 