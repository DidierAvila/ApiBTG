using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ApiBTG.Infrastructure.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
        public ClienteRepository(BGTDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> GetClientesWithInscripcionesAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(c => c.Inscripciones)
                .ThenInclude(i => i.Producto)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Cliente>> GetClientesWithVisitasAsync(CancellationToken cancellationToken)
        {
            return await EntitySet
                .Include(c => c.Visitas)
                .ThenInclude(v => v.Sucursal)
                .ToListAsync(cancellationToken);
        }
    }
}
