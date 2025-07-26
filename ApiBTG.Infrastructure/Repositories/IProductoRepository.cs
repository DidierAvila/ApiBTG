using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IProductoRepository : IRepositoryBase<Producto>
    {
        Task<IEnumerable<Producto>> GetProductosWithDisponibilidadesAsync(CancellationToken cancellationToken);
    }
} 