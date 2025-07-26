using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IVisitaRepository : IRepositoryBase<Visitan>
    {
        Task<IEnumerable<Visitan>> GetVisitasWithDetailsAsync(CancellationToken cancellationToken);
        Task<Visitan?> GetVisitaByIdsAsync(int idSucursal, int idCliente, CancellationToken cancellationToken);
        Task<bool> ExistsVisitaAsync(int idSucursal, int idCliente, CancellationToken cancellationToken);
    }
} 