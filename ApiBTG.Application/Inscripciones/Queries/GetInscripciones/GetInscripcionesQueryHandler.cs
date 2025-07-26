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
                IdProducto = i.IdProducto,
                IdCliente = i.IdCliente,
                Cliente = new ClienteDto
                {
                    Id = i.Cliente.Id,
                    Nombre = i.Cliente.Nombre,
                    Apellidos = i.Cliente.Apellidos,
                    Ciudad = i.Cliente.Ciudad,
                    Monto = i.Cliente.Monto
                },
                Producto = new ProductoDto
                {
                    Id = i.Producto.Id,
                    Nombre = i.Producto.Nombre,
                    TipoProducto = i.Producto.TipoProducto
                }
            });
        }
    }
} 