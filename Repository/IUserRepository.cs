using backend.Models;

namespace backend.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<User> GetUserById(string userId, CancellationToken cancellationToken);
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
        Task CreateUser(User user, CancellationToken cancellationToken);
        Task<bool> DeleteUser(string userId, CancellationToken cancellationToken);
        Task<bool> UpdateUser(User user, CancellationToken cancellationToken);
    }
}
