using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.CreateDisponibilidad
{
    public class CreateDisponibilidadCommandHandler : IRequestHandler<CreateDisponibilidadCommand, DisponibilidadDto>
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;
        private readonly ISucursalRepository _sucursalRepository;
        private readonly IProductoRepository _productoRepository;

        public CreateDisponibilidadCommandHandler(
            IDisponibilidadRepository disponibilidadRepository,
            ISucursalRepository sucursalRepository,
            IProductoRepository productoRepository)
        {
            _disponibilidadRepository = disponibilidadRepository;
            _sucursalRepository = sucursalRepository;
            _productoRepository = productoRepository;
        }

        public async Task<DisponibilidadDto> Handle(CreateDisponibilidadCommand request, CancellationToken cancellationToken)
        {
            // Verificar que la sucursal existe
            var sucursal = await _sucursalRepository.GetByID(request.IdSucursal, cancellationToken);
            if (sucursal == null)
            {
                throw new KeyNotFoundException($"Sucursal con ID {request.IdSucursal} no encontrada");
            }

            // Verificar que el producto existe
            var producto = await _productoRepository.GetByID(request.IdProducto, cancellationToken);
            if (producto == null)
            {
                throw new KeyNotFoundException($"Producto con ID {request.IdProducto} no encontrado");
            }

            // Verificar que no existe ya una disponibilidad
            var exists = await _disponibilidadRepository.ExistsDisponibilidadAsync(request.IdSucursal, request.IdProducto, cancellationToken);
            if (exists)
            {
                throw new InvalidOperationException($"Ya existe una disponibilidad para la sucursal {request.IdSucursal} y el producto {request.IdProducto}");
            }

            var disponibilidad = new Disponibilidad
            {
                IdSucursal = request.IdSucursal,
                IdProducto = request.IdProducto,
                MontoMinimo = request.MontoMinimo
            };

            var createdDisponibilidad = await _disponibilidadRepository.Create(disponibilidad, cancellationToken);

            return new DisponibilidadDto
            {
                Id = createdDisponibilidad.Id,
                IdSucursal = createdDisponibilidad.IdSucursal,
                IdProducto = createdDisponibilidad.IdProducto,
                MontoMinimo = createdDisponibilidad.MontoMinimo,
                Sucursal = new SucursalDto
                {
                    Id = sucursal.Id,
                    Nombre = sucursal.Nombre,
                    Ciudad = sucursal.Ciudad
                },
                Producto = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    TipoProducto = producto.TipoProducto
                }
            };
        }
    }
} 