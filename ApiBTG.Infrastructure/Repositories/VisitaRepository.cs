using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class VisitaRepository : RepositoryBase<Visita>, IVisitaRepository
    {
        public VisitaRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Visita>> GetVisitasWithDetailsAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(v => v.Sucursal)
                .Include(v => v.Cliente)
                .ToListAsync(cancellationToken);
        }

        public async Task<Visita?> GetVisitaByIdsAsync(int idSucursal, int idCliente, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(v => v.Sucursal)
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.IdSucursal == idSucursal && v.IdCliente == idCliente, cancellationToken);
        }

        public async Task<bool> ExistsVisitaAsync(int idSucursal, int idCliente, CancellationToken cancellationToken)
        {
            return await EntitySet.AnyAsync(v => v.IdSucursal == idSucursal && v.IdCliente == idCliente, cancellationToken);
        }
    }
} 