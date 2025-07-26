using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Inscripciones.Commands.DeleteInscripcion
{
    public class DeleteInscripcionCommandHandler : IRequestHandler<DeleteInscripcionCommand, bool>
    {
        private readonly IInscripcionRepository _inscripcionRepository;
        private readonly IClienteRepository _clienteRepository;

        public DeleteInscripcionCommandHandler(
            IInscripcionRepository inscripcionRepository, 
            IClienteRepository clienteRepository)
        {
            _inscripcionRepository = inscripcionRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(DeleteInscripcionCommand request, CancellationToken cancellationToken)
        {
            var inscripcion = await _inscripcionRepository.GetByID(request.Id, cancellationToken);
            if (inscripcion == null)
                return false;

            var result = await _inscripcionRepository.Delete(inscripcion.Id, cancellationToken);
            if (result != null)
            {
                var cliente = await _clienteRepository.GetByID(inscripcion.IdCliente, cancellationToken);
                if (cliente != null && inscripcion.Disponibilidad != null)
                {
                    cliente.Monto += inscripcion.Disponibilidad.MontoMinimo;
                    await _clienteRepository.Update(cliente, cancellationToken);
                }
                return true;
            }
            return false;
        }
    }
} 