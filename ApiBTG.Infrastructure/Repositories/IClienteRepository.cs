using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<IEnumerable<Cliente>> GetClientesWithInscripcionesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Cliente>> GetClientesWithVisitasAsync(CancellationToken cancellationToken);
    }
} 