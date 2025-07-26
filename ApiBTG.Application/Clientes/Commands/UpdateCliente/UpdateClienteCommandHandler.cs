using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteCommandHandler : IRequestHandler<UpdateClienteCommand, ClienteDto>
    {
        private readonly IClienteRepository _clienteRepository;

        public UpdateClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDto> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetByID(request.Id, cancellationToken);

            if (cliente == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {request.Id} no encontrado");
            }

            cliente.Nombre = request.Nombre;
            cliente.Apellidos = request.Apellidos;
            cliente.Ciudad = request.Ciudad;
            cliente.Monto = request.Monto;
            cliente.UsuarioId = request.UsuarioId;

            await _clienteRepository.Update(cliente, cancellationToken);

            return new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Ciudad = cliente.Ciudad,
                Monto = cliente.Monto
            };
        }
    }
} 