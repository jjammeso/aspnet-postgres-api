using MongoDB.Driver;
using RestApiTemplate.Models;

namespace RestApiTemplate.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoConnection"));
            _database = client.GetDatabase("RestApiTemplate");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}
