using SecurePFX.Domain.Entities;

namespace SecurePFX.Domain.Interfaces.Repositories
{
    public interface ICertificateRepository
    {
        Task<string> AddAsync(Certificate certificate, CancellationToken cancellationToken = default);
    }
}
