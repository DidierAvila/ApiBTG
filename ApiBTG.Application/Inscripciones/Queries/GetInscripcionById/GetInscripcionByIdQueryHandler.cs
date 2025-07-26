using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Queries.GetInscripcionById
{
    public class GetInscripcionByIdQueryHandler : IRequestHandler<GetInscripcionByIdQuery, InscripcionDto?>
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public GetInscripcionByIdQueryHandler(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<InscripcionDto?> Handle(GetInscripcionByIdQuery request, CancellationToken cancellationToken)
        {
            var inscripcion = await _inscripcionRepository.GetByID(request.Id, cancellationToken);

            if (inscripcion == null)
                return null;

            return new InscripcionDto
            {
                Id = inscripcion.Id,
                IdCliente = inscripcion.IdCliente,
                IdDisponibilidad = inscripcion.IdDisponibilidad,
                Cliente = new ClienteDto
                {
                    Id = inscripcion.Cliente.Id,
                    Nombre = inscripcion.Cliente.Nombre,
                    Apellidos = inscripcion.Cliente.Apellidos,
                    Ciudad = inscripcion.Cliente.Ciudad,
                    Monto = inscripcion.Cliente.Monto
                },
                Disponibilidad = new DisponibilidadDto
                {
                    Id = inscripcion.Disponibilidad.Id,
                    IdSucursal = inscripcion.Disponibilidad.IdSucursal,
                    IdProducto = inscripcion.Disponibilidad.IdProducto,
                    MontoMinimo = inscripcion.Disponibilidad.MontoMinimo,
                    Sucursal = new SucursalDto
                    {
                        Id = inscripcion.Disponibilidad.Sucursal.Id,
                        Nombre = inscripcion.Disponibilidad.Sucursal.Nombre,
                        Ciudad = inscripcion.Disponibilidad.Sucursal.Ciudad
                    },
                    Producto = new ProductoDto
                    {
                        Id = inscripcion.Disponibilidad.Producto.Id,
                        Nombre = inscripcion.Disponibilidad.Producto.Nombre,
                        TipoProducto = inscripcion.Disponibilidad.Producto.TipoProducto
                    }
                }
            };
        }
    }
} 