using MongoDB.Driver;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Infrastructure.Data.Contexts;

namespace SecurePFX.Infrastructure.Data.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly IMongoCollection<Certificate> _collection;

        public CertificateRepository(MongoDbContext context)
        {
            _collection = context.Certificates;
        }

        public async Task<string> AddAsync(Certificate certificate, CancellationToken cancellationToken = default)
        {
            await _collection.InsertOneAsync(certificate, cancellationToken: cancellationToken);

            return certificate.Id;
        }
    }
}
