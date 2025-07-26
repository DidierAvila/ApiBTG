using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface ISucursalRepository : IRepositoryBase<Sucursal>
    {
        Task<IEnumerable<Sucursal>> GetSucursalesWithDisponibilidadesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Sucursal>> GetSucursalesWithVisitasAsync(CancellationToken cancellationToken);
    }
} 