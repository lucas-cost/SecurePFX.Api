using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Enums;
using SecurePFX.Domain.Interfaces;
using SecurePFX.Infrastructure.Configurations;

namespace SecurePFX.Infrastructure.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly IMongoCollection<Certificate> _collection;

        public CertificateRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Certificate>(settings.Value.CertificateCollectionName);
        }

        public async Task InsertAsync(Certificate entity, CancellationToken cancellationToken = default)
            => await _collection.InsertOneAsync(entity, null, cancellationToken);

        public async Task UpdateAsync(Guid id, Certificate entity, CancellationToken cancellationToken = default)
            => await _collection.ReplaceOneAsync(c => c.Id == id, entity, cancellationToken: cancellationToken);

        public async Task<Certificate?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _collection.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task<Certificate?> GetActiveAsync(CertificateContext context, CertificateCategory category, CancellationToken cancellationToken = default)
            => await _collection.Find(c => c.IsActive &&
                                           c.Metadata.Context == context &&
                                           c.Metadata.Category == category)
                                .FirstOrDefaultAsync(cancellationToken);
    }
}
