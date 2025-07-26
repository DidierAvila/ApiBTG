using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteCommandHandler : IRequestHandler<DeleteClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public DeleteClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {
            var deletedEntity = await _clienteRepository.Delete(request.Id, cancellationToken);
            return deletedEntity != null;
        }
    }
} 