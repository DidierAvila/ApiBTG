using ApiBTG.Domain.Entities;
using ApiBTG.Infrastructure.Repositories;

namespace ApiBTG.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<Usuario> _UserRepository;

        public UserService(IRepositoryBase<Usuario> userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<Usuario> Create(Usuario createUser, CancellationToken cancellationToken)
        {
            return await _UserRepository.Create(createUser, cancellationToken);
        }

        public async Task<Usuario?> Get(int id, CancellationToken cancellationToken)
        {
            Usuario? CurrentUser = await _UserRepository.GetByID(id, cancellationToken);
            if (CurrentUser != null)
            {
                return CurrentUser;
            }
            return null;
        }

        public async Task<Usuario?> Update(Usuario updateRequest, CancellationToken cancellationToken)
        {
            Usuario? entity = await _UserRepository.GetByID(updateRequest.Id, cancellationToken);
            await _UserRepository.Update(updateRequest, cancellationToken);

            return entity;
        }

        public async Task<ICollection<Usuario>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<Usuario> CurrentUsers = [];
            CurrentUsers = await _UserRepository.GetAll(cancellationToken);
            if (CurrentUsers != null)
            {
                return (ICollection<Usuario>)CurrentUsers;
            }
            return CurrentUsers!.ToList();
        }
    }
}
