using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidadById
{
    public class GetDisponibilidadByIdQueryHandler : IRequestHandler<GetDisponibilidadByIdQuery, DisponibilidadDto?>
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public GetDisponibilidadByIdQueryHandler(IDisponibilidadRepository disponibilidadRepository)
        {
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<DisponibilidadDto?> Handle(GetDisponibilidadByIdQuery request, CancellationToken cancellationToken)
        {
            var disponibilidad = await _disponibilidadRepository.GetDisponibilidadByIdsAsync(request.IdSucursal, request.IdProducto, cancellationToken);

            if (disponibilidad == null)
                return null;

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