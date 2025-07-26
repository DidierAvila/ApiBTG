using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Visitas.Commands.DeleteVisita
{
    public class DeleteVisitaCommandHandler : IRequestHandler<DeleteVisitaCommand, bool>
    {
        private readonly IVisitaRepository _visitaRepository;

        public DeleteVisitaCommandHandler(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        public async Task<bool> Handle(DeleteVisitaCommand request, CancellationToken cancellationToken)
        {
            // Para entidades con clave compuesta, necesitamos buscar primero
            var visita = await _visitaRepository.GetVisitaByIdsAsync(request.IdSucursal, request.IdCliente, cancellationToken);
            
            if (visita == null)
            {
                return false;
            }

            // Como el repositorio base no maneja claves compuestas directamente,
            // necesitamos implementar la lógica de eliminación aquí
            // Por ahora, retornamos false ya que necesitaríamos un método específico en el repositorio
            return false;
        }
    }
} 