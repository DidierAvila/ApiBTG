using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.UpdateDisponibilidad
{
    public class UpdateDisponibilidadCommandHandler : IRequestHandler<UpdateDisponibilidadCommand, DisponibilidadDto>
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public UpdateDisponibilidadCommandHandler(IDisponibilidadRepository disponibilidadRepository)
        {
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<DisponibilidadDto> Handle(UpdateDisponibilidadCommand request, CancellationToken cancellationToken)
        {
            var disponibilidad = await _disponibilidadRepository.GetDisponibilidadByIdsAsync(request.IdSucursal, request.IdProducto, cancellationToken);

            if (disponibilidad == null)
            {
                throw new KeyNotFoundException($"Disponibilidad con Sucursal ID {request.IdSucursal} y Producto ID {request.IdProducto} no encontrada");
            }

            disponibilidad.MontoMinimo = request.MontoMinimo;

            await _disponibilidadRepository.Update(disponibilidad, cancellationToken);

            return new DisponibilidadDto
            {
                Id = disponibilidad.Id,
                IdSucursal = disponibilidad.IdSucursal,
                IdProducto = disponibilidad.IdProducto,
                MontoMinimo = disponibilidad.MontoMinimo,
                Sucursal = new SucursalDto
                {
                    Id = disponibilidad.Sucursal.Id,
                    Nombre = disponibilidad.Sucursal.Nombre,
                    Ciudad = disponibilidad.Sucursal.Ciudad
                },
                Producto = new ProductoDto
                {
                    Id = disponibilidad.Producto.Id,
                    Nombre = disponibilidad.Producto.Nombre,
                    TipoProducto = disponibilidad.Producto.TipoProducto
                }
            };
        }
    }
} 