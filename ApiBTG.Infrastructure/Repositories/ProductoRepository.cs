using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class ProductoRepository : RepositoryBase<Producto>, IProductoRepository
    {
        public ProductoRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Producto>> GetProductosWithInscripcionesAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(p => p.Inscripciones)
                .ThenInclude(i => i.Cliente)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Producto>> GetProductosWithDisponibilidadesAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(p => p.Disponibilidades)
                .ThenInclude(d => d.Sucursal)
                .ToListAsync(cancellationToken);
        }
    }
} 