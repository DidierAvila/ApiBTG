using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Queries.GetDisponibilidades
{
    public class GetDisponibilidadesQueryHandler : IRequestHandler<GetDisponibilidadesQuery, IEnumerable<DisponibilidadDto>>
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public GetDisponibilidadesQueryHandler(IDisponibilidadRepository disponibilidadRepository)
        {
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<IEnumerable<DisponibilidadDto>> Handle(GetDisponibilidadesQuery request, CancellationToken cancellationToken)
        {
            var disponibilidades = await _disponibilidadRepository.GetDisponibilidadesWithDetailsAsync(cancellationToken);

            return disponibilidades.Select(d => new DisponibilidadDto
            {
                Id = d.Id,
                IdSucursal = d.IdSucursal,
                IdProducto = d.IdProducto,
                MontoMinimo = d.MontoMinimo,
                Sucursal = new SucursalDto
                {
                    Id = d.Sucursal.Id,
                    Nombre = d.Sucursal.Nombre,
                    Ciudad = d.Sucursal.Ciudad
                },
                Producto = new ProductoDto
                {
                    Id = d.Producto.Id,
                    Nombre = d.Producto.Nombre,
                    TipoProducto = d.Producto.TipoProducto
                }
            });
        }
    }
} 