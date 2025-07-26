using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.UpdateInscripcion
{
    public class UpdateInscripcionCommandHandler : IRequestHandler<UpdateInscripcionCommand, InscripcionDto>
    {
        private readonly IInscripcionRepository _inscripcionRepository;

        public UpdateInscripcionCommandHandler(IInscripcionRepository inscripcionRepository)
        {
            _inscripcionRepository = inscripcionRepository;
        }

        public async Task<InscripcionDto> Handle(UpdateInscripcionCommand request, CancellationToken cancellationToken)
        {
            var inscripcion = await _inscripcionRepository.GetInscripcionByIdsAsync(request.IdProducto, request.IdCliente, cancellationToken);

            if (inscripcion == null)
            {
                throw new KeyNotFoundException($"Inscripción con Producto ID {request.IdProducto} y Cliente ID {request.IdCliente} no encontrada");
            }

            await _inscripcionRepository.Update(inscripcion, cancellationToken);

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