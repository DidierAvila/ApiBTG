using ApiBTG.Domain.Entities;
using ApiBTG.Domain.Dtos;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesWithInscripcionesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Cliente>> GetClientesWithVisitasAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ClienteInscripcionDto>> GetClientesConInscripcionesEnSucursalesVisitadasAsync(
            int? clienteId = null, 
            int? sucursalId = null, 
            CancellationToken cancellationToken = default);
    }
} 