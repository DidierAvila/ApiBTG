using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Disponibilidades.Commands.DeleteDisponibilidad
{
    public class DeleteDisponibilidadCommandHandler : IRequestHandler<DeleteDisponibilidadCommand, bool>
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public DeleteDisponibilidadCommandHandler(IDisponibilidadRepository disponibilidadRepository)
        {
            _disponibilidadRepository = disponibilidadRepository;
        }

        public async Task<bool> Handle(DeleteDisponibilidadCommand request, CancellationToken cancellationToken)
        {
            // Para entidades con clave compuesta, necesitamos buscar primero
            var disponibilidad = await _disponibilidadRepository.GetDisponibilidadByIdsAsync(request.IdSucursal, request.IdProducto, cancellationToken);
            
            if (disponibilidad == null)
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