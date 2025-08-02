using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePFX.Domain.Entities;
using SecurePFX.Infrastructure.Settings;

namespace SecurePFX.Infrastructure.Data.Contexts
{
    public sealed class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Certificate> Certificates =>
            _database.GetCollection<Certificate>("certificates");
    }
}
