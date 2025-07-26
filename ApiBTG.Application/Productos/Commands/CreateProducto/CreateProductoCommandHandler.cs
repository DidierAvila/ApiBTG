using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Productos.Commands.CreateProducto
{
    public class CreateProductoCommandHandler : IRequestHandler<CreateProductoCommand, ProductoDto>
    {
        private readonly IProductoRepository _productoRepository;

        public CreateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = new Producto
            {
                Nombre = request.Nombre,
                TipoProducto = request.TipoProducto
            };

            var createdProducto = await _productoRepository.Create(producto, cancellationToken);

            return new ProductoDto
            {
                Id = createdProducto.Id,
                Nombre = createdProducto.Nombre,
                TipoProducto = createdProducto.TipoProducto
            };
        }
    }
} 