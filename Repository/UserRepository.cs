using backend.Models;
using MongoDB.Driver;
namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _userCollection;
        public UserRepository(MongoContext context)
        {
            _userCollection = context.GetCollection<User>(CollectionConstants.UserCollection);
        }
        /// <summary>
        ///     Get all users async
        /// </summary>
        /// <returns>IEnumerable of users</returns>
        public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            var users = await _userCollection.FindAsync(Builders<User>.Filter.Empty, cancellationToken: cancellationToken);
            return await users.ToListAsync();
        }

        /// <summary>
        ///     Get user by id async
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> GetUserById(string userId, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Where(user => user.Id.ToString() == userId);
            var user = await _userCollection.FindAsync(filter, cancellationToken: cancellationToken);
            return await user.FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Get user by Email async
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Where(user => user.Email == email);
            var user = await _userCollection.FindAsync(filter, cancellationToken: cancellationToken);
            return await user.FirstOrDefaultAsync();
        }

        /// <summary>
        ///    Create user async
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateUser(User user, CancellationToken cancellationToken)
        {
            await _userCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
        }

        /// <summary>
        ///     Delete User async
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUser(string userId, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Where(user => user.Id.ToString() == userId);
            var result = await _userCollection.DeleteOneAsync(filter, cancellationToken);
            return result.DeletedCount > 0;
        }

        /// <summary>
        ///     Update user async
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(User user, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Where(u => u.Id == user.Id);
            var updatedUser = await _userCollection.ReplaceOneAsync(filter, user, cancellationToken: cancellationToken);
            return updatedUser.ModifiedCount > 0;
        }
    }
}
