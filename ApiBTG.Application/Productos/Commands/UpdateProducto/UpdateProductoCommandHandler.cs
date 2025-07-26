using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Productos.Commands.UpdateProducto
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, ProductoDto>
    {
        private readonly IProductoRepository _productoRepository;

        public UpdateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDto> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetByID(request.Id, cancellationToken);

            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto con ID {request.Id} no encontrado");
            }

            producto.Nombre = request.Nombre;
            producto.TipoProducto = request.TipoProducto;

            await _productoRepository.Update(producto, cancellationToken);

            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                TipoProducto = producto.TipoProducto
            };
        }
    }
} 