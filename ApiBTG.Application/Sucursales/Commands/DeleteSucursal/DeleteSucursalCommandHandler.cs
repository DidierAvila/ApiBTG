using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Sucursales.Commands.DeleteSucursal
{
    public class DeleteSucursalCommandHandler : IRequestHandler<DeleteSucursalCommand, bool>
    {
        private readonly ISucursalRepository _sucursalRepository;

        public DeleteSucursalCommandHandler(ISucursalRepository sucursalRepository)
        {
            _sucursalRepository = sucursalRepository;
        }

        public async Task<bool> Handle(DeleteSucursalCommand request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _sucursalRepository.Delete(request.Id, cancellationToken);
            return deletedEntity != null;
        }
    }
} 