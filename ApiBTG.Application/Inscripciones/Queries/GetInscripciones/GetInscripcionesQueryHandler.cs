using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Queries.GetInscripciones
{
    public class GetInscripcionesQueryHandler : IRequestHandler<GetInscripcionesQuery, IEnumerable<InscripcionDto>>
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public GetInscripcionesQueryHandler(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<IEnumerable<InscripcionDto>> Handle(GetInscripcionesQuery request, CancellationToken cancellationToken)
        {
            var inscripciones = await _inscripcionRepository.GetInscripcionesWithDetailsAsync(cancellationToken);

            return inscripciones.Select(i => new InscripcionDto
            {
                Id = i.Id,
                IdCliente = i.IdCliente,
                IdDisponibilidad = i.IdDisponibilidad,
                Cliente = new ClienteDto
                {
                    Id = i.Cliente.Id,
                    Nombre = i.Cliente.Nombre,
                    Apellidos = i.Cliente.Apellidos,
                    Ciudad = i.Cliente.Ciudad,
                    Monto = i.Cliente.Monto
                },
                Disponibilidad = new DisponibilidadDto
                {
                    Id = i.Disponibilidad.Id,
                    IdSucursal = i.Disponibilidad.IdSucursal,
                    IdProducto = i.Disponibilidad.IdProducto,
                    MontoMinimo = i.Disponibilidad.MontoMinimo,
                    Sucursal = new SucursalDto
                    {
                        Id = i.Disponibilidad.Sucursal.Id,
                        Nombre = i.Disponibilidad.Sucursal.Nombre,
                        Ciudad = i.Disponibilidad.Sucursal.Ciudad
                    },
                    Producto = new ProductoDto
                    {
                        Id = i.Disponibilidad.Producto.Id,
                        Nombre = i.Disponibilidad.Producto.Nombre,
                        TipoProducto = i.Disponibilidad.Producto.TipoProducto
                    }
                }
            });
        }
    }
} 