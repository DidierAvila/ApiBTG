using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IDisponibilidadRepository : IRepositoryBase<Disponibilidad>
    {
        Task<IEnumerable<Disponibilidad>> GetDisponibilidadesWithDetailsAsync(CancellationToken cancellationToken);
        Task<Disponibilidad?> GetDisponibilidadByIdsAsync(int idSucursal, int idProducto, CancellationToken cancellationToken);
        Task<bool> ExistsDisponibilidadAsync(int idSucursal, int idProducto, CancellationToken cancellationToken);
        Task<IEnumerable<Disponibilidad>> GetDisponibilidadesByProductoAsync(int idProducto, CancellationToken cancellationToken);
    }
} 