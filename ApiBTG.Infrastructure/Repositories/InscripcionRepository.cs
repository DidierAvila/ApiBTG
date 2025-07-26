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
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Sucursal)
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<Inscripcion?> GetInscripcionByIdsAsync(int idCliente, int idDisponibilidad, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(i => i.Cliente)
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Sucursal)
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(i => i.IdCliente == idCliente && i.IdDisponibilidad == idDisponibilidad, cancellationToken);
        }

        public async Task<Inscripcion?> GetByID(int id, CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(i => i.Cliente)
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Sucursal)
                .Include(i => i.Disponibilidad)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task<bool> ExistsInscripcionAsync(int idCliente, int idDisponibilidad, CancellationToken cancellationToken)
        {
            return await EntitySet.AnyAsync(i => i.IdCliente == idCliente && i.IdDisponibilidad == idDisponibilidad, cancellationToken);
        }
    }
} 