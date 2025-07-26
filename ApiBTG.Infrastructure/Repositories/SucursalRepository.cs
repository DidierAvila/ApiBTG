using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class SucursalRepository : RepositoryBase<Sucursal>, ISucursalRepository
    {
        public SucursalRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Sucursal>> GetSucursalesWithDisponibilidadesAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(s => s.Disponibilidades)
                .ThenInclude(d => d.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sucursal>> GetSucursalesWithVisitasAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(s => s.Visitas)
                .ThenInclude(v => v.Cliente)
                .ToListAsync(cancellationToken);
        }
    }
} 