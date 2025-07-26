using ApiBTG.Domain.Dtos;
using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Clientes.Commands.CreateCliente
{
    public class CreateClienteCommandHandler : IRequestHandler<CreateClienteCommand, ClienteDto>
    {
        private readonly IClienteRepository _clienteRepository;

        public CreateClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ClienteDto> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = new Cliente
            {
                Nombre = request.Nombre,
                Apellidos = request.Apellidos,
                Ciudad = request.Ciudad,
                Monto = request.Monto
            };

            // Regla de negocio: Validar que el monto inicial de COP $500.000.
            cliente.Monto = 500000;

            var createdCliente = await _clienteRepository.Create(cliente, cancellationToken);

            return new ClienteDto
            {
                Id = createdCliente.Id,
                Nombre = createdCliente.Nombre,
                Apellidos = createdCliente.Apellidos,
                Ciudad = createdCliente.Ciudad,
                Monto = createdCliente.Monto
            };
        }
    }
} 