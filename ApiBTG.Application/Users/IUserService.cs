using ApiBTG.Domain.Entities;

namespace ApiBTG.Application.Users
{
    public interface IUserService
    {
        Task<Usuario> Create(Usuario createUser, CancellationToken cancellationToken);
        Task<Usuario?> Get(int id, CancellationToken cancellationToken);
        Task<ICollection<Usuario>> GetAll(CancellationToken cancellationToken);
        Task<Usuario?> Update(Usuario updateRequest, CancellationToken cancellationToken);
    }
}
