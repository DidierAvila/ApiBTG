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
            var inscripcion = await _inscripcionRepository.GetInscripcionByIdsAsync(request.IdProducto, request.IdCliente, cancellationToken);

            if (inscripcion == null)
                return null;

            return new InscripcionDto
            {
                Id = inscripcion.Id,
                IdProducto = inscripcion.IdProducto,
                IdCliente = inscripcion.IdCliente,
                Cliente = new ClienteDto
                {
                    Id = inscripcion.Cliente.Id,
                    Nombre = inscripcion.Cliente.Nombre,
                    Apellidos = inscripcion.Cliente.Apellidos,
                    Ciudad = inscripcion.Cliente.Ciudad,
                    Monto = inscripcion.Cliente.Monto
                },
                Producto = new ProductoDto
                {
                    Id = inscripcion.Producto.Id,
                    Nombre = inscripcion.Producto.Nombre,
                    TipoProducto = inscripcion.Producto.TipoProducto
                }
            };
        }
    }
} 