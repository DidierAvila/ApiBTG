using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion
{
    public class DeleteInscripcionCommandHandler : IRequestHandler<DeleteInscripcionCommand, bool>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVisitaRepository _visitaRepository;

        public DeleteInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository, 
            IClienteRepository clienteRepository,
            IVisitaRepository visitaRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
            _visitaRepository = visitaRepository;
        }

        public async Task<bool> Handle(DeleteInscripcionCommand request, CancellationToken cancellationToken)
        {
            var inscripcion = await _inscripcionRepository.GetByID(request.Id, cancellationToken);
            if (inscripcion == null)
                return false;

            Inscripcion? result = await _inscripcionRepository.Delete(inscripcion.Id, cancellationToken);
            if (result != null)
            {
                // Actualizar el monto del cliente
                var cliente = await _clienteRepository.GetByID(inscripcion.IdCliente, cancellationToken);
                if (cliente != null && inscripcion.Disponibilidad != null)
                {
                    cliente.Monto += inscripcion.Disponibilidad.MontoMinimo;
                    await _clienteRepository.Update(cliente, cancellationToken);
                }

                // Registrar la inscripción en el historial de transacciones
                var visita = new Visita
                {
                    IdSucursal = result.Disponibilidad.IdSucursal,
                    IdCliente = result.Cliente.Id,
                    FechaVisita = DateTime.UtcNow,
                    TipoAccion = "Cancelación",
                };
                await _visitaRepository.Create(visita, cancellationToken);

                return true;
            }
            return false;
        }
    }
} 