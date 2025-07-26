using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IInscripcionRepository : IRepositoryBase<Inscripcion>
    {
        Task<IEnumerable<Inscripcion>> GetInscripcionesWithDetailsAsync(CancellationToken cancellationToken);
        Task<Inscripcion?> GetInscripcionByIdsAsync(int idProducto, int idCliente, CancellationToken cancellationToken);
        Task<bool> ExistsInscripcionAsync(int idProducto, int idCliente, CancellationToken cancellationToken);
    }
} 