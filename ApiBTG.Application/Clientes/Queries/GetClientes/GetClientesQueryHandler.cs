using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Clientes.Queries.GetClientes
{
    public class GetClientesQueryHandler : IRequestHandler<GetClientesQuery, IEnumerable<ClienteDto>>
    {
        private readonly IClienteRepository _clienteRepository;

        public GetClientesQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDto>> Handle(GetClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.GetAll(cancellationToken);

            return clientes.Select(cliente => new ClienteDto
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Ciudad = cliente.Ciudad,
                Monto = cliente.Monto,
                UsuarioId = cliente.UsuarioId
            }).ToList();
        }
    }
} 