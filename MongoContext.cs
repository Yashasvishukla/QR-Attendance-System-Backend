using MongoDB.Driver;

namespace backend
{
    // MongoContext Singleton class per database
    public class MongoContext
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private readonly IConfiguration? _configuration;

        public MongoContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MongoClient(_configuration?.GetConnectionString("mongoDbConnection"));
            _database = _client.GetDatabase(_configuration?.GetConnectionString("databaseName"));
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }



    }
}
