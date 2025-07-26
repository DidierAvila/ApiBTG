using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class InscripcionRepository : RepositoryBase<Inscripcion>, IInscripcionRepository
    {
        public InscripcionRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Inscripcion>> GetInscripcionesWithDetailsAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(i => i.Cliente)
                .Include(i => i.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<Inscripcion?> GetInscripcionByIdsAsync(int idProducto, int idCliente, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(i => i.Cliente)
                .Include(i => i.Producto)
                .FirstOrDefaultAsync(i => i.IdProducto == idProducto && i.IdCliente == idCliente, cancellationToken);
        }

        public async Task<bool> ExistsInscripcionAsync(int idProducto, int idCliente, CancellationToken cancellationToken)
        {
            return await EntitySet.AnyAsync(i => i.IdProducto == idProducto && i.IdCliente == idCliente, cancellationToken);
        }
    }
} 