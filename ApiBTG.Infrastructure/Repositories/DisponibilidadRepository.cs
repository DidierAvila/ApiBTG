using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class DisponibilidadRepository : RepositoryBase<Disponibilidad>, IDisponibilidadRepository
    {
        public DisponibilidadRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Disponibilidad>> GetDisponibilidadesWithDetailsAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(d => d.Sucursal)
                .Include(d => d.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<Disponibilidad?> GetDisponibilidadByIdsAsync(int idSucursal, int idProducto, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(d => d.Sucursal)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(d => d.IdSucursal == idSucursal && d.IdProducto == idProducto, cancellationToken);
        }

        public async Task<bool> ExistsDisponibilidadAsync(int idSucursal, int idProducto, CancellationToken cancellationToken)
        {
            return await EntitySet.AnyAsync(d => d.IdSucursal == idSucursal && d.IdProducto == idProducto, cancellationToken);
        }

        public async Task<IEnumerable<Disponibilidad>> GetDisponibilidadesByProductoAsync(int idProducto, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(d => d.Sucursal)
                .Include(d => d.Producto)
                .Where(d => d.IdProducto == idProducto)
                .ToListAsync(cancellationToken);
        }
    }
} 