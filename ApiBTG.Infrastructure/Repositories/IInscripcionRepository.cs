using ApiBTG.Domain.Entities;

namespace ApiBTG.Infrastructure.Repositories
{
    public interface IInscripcionRepository : IRepositoryBase<Inscripcion>
    {
        Task<IEnumerable<Inscripcion>> GetInscripcionesWithDetailsAsync(CancellationToken cancellationToken);
        Task<Inscripcion?> GetInscripcionByIdsAsync(int idCliente, int idDisponibilidad, CancellationToken cancellationToken);
        Task<bool> ExistsInscripcionAsync(int idCliente, int idDisponibilidad, CancellationToken cancellationToken);
    }
} 