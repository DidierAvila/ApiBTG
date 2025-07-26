using ApiBTG.Domain.Dtos.Security;

namespace ApiBTG.Application.Security
{
    public interface ISecurityService
    {
        Task<LoginResponse> Login(LoginRequest autorizacion, CancellationToken cancellationToken);
    }
}
