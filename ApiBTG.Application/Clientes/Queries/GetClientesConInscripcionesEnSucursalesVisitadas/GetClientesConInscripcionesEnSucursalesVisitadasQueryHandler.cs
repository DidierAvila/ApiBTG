using ApiBTG.Domain.Dtos;
using ApiBTG.Infrastructure.Repositories;
using MediatR;

namespace ApiBTG.Application.Clientes.Queries.GetClientesConInscripcionesEnSucursalesVisitadas
{
    public class GetClientesConInscripcionesEnSucursalesVisitadasQueryHandler 
        : IRequestHandler<GetClientesConInscripcionesEnSucursalesVisitadasQuery, IEnumerable<ClienteInscripcionDto>>
    {
        private readonly IClienteRepository _clienteRepository;

        public GetClientesConInscripcionesEnSucursalesVisitadasQueryHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteInscripcionDto>> Handle(
            GetClientesConInscripcionesEnSucursalesVisitadasQuery request, 
            CancellationToken cancellationToken)
        {
            return await _clienteRepository.GetClientesConInscripcionesEnSucursalesVisitadasAsync(
                request.ClienteId, 
                request.SucursalId, 
                cancellationToken);
        }
    }
} 