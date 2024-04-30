using backend.Models;

namespace backend.Services
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password, CancellationToken cancellationToken);
        Task<string?> Login(string email, string password, CancellationToken cancellationToken);
    }
}
