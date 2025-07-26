using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IVisitaRepository : IRepositoryBase<Visita>
    {
        Task<IEnumerable<Visita>> GetVisitasWithDetailsAsync(CancellationToken cancellationToken);
        Task<Visita?> GetVisitaByIdsAsync(int idSucursal, int idCliente, CancellationToken cancellationToken);
        Task<bool> ExistsVisitaAsync(int idSucursal, int idCliente, CancellationToken cancellationToken);
    }
} 