using MongoDB.Driver;
using RestApiTemplate.Database;
using RestApiTemplate.Models.Mongo;

namespace RestApiTemplate.Repositories.Mongo
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoCollection<MongoRefreshToken> _collection;

        public RefreshTokenRepository(MongoDbContext context )
        {
            _collection = context.RefreshTokens;
        }
        public async Task AddAsync(MongoRefreshToken token)
        {
            await _collection.InsertOneAsync( token );
        }

        public async Task<MongoRefreshToken?> GetByTokenAsync(string token)
        {
            return await _collection.Find(t => t.Token == token).FirstOrDefaultAsync();
        }

        public async Task RevokeAsync(string token)
        {
            var update = Builders<MongoRefreshToken>.Update
            .Set(t => t.IsRevoked, true)
            .Set(t => t.RevokedAt, DateTime.UtcNow);

            await _collection.UpdateOneAsync(t => t.Token == token, update);
        }
    }
}
