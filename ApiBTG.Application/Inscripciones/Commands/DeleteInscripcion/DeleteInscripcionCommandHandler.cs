using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion
{
    public class DeleteInscripcionCommandHandler : IRequestHandler<DeleteInscripcionCommand, bool>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public DeleteInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository, 
            IClienteRepository clienteRepository,
            IDisponibilidadRepository disponibilidadRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<bool> Handle(DeleteInscripcionCommand request, CancellationToken cancellationToken)
        {
            // Para entidades con clave compuesta, necesitamos buscar primero
            var inscripcion = await _inscripcionRepository.GetInscripcionByIdsAsync(request.IdProducto, request.IdCliente, cancellationToken);
            
            if (inscripcion == null)
            {
                return false;
            }

            // Buscar la disponibilidad del producto para obtener el monto mínimo
            var disponibilidades = await _disponibilidadRepository.GetDisponibilidadesByProductoAsync(request.IdProducto, cancellationToken);
            var montoMinimo = disponibilidades.FirstOrDefault()?.MontoMinimo ?? 0m;

            // Eliminar la inscripción
            var result = await _inscripcionRepository.Delete(inscripcion.Id, cancellationToken);
            if (result != null)
            {
                // Actualizar el monto del cliente
                var cliente = await _clienteRepository.GetByID(request.IdCliente, cancellationToken);
                if (cliente != null)
                {
                    cliente.Monto += montoMinimo; // Reembolsar el monto mínimo
                    await _clienteRepository.Update(cliente, cancellationToken);
                }

                return true;
            }

            return false;
        }
    }
} 