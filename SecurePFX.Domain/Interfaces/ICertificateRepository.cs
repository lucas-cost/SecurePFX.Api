using SecurePFX.Domain.Entities;

namespace SecurePFX.Domain.Interfaces
{
    public interface ICertificateRepository
    {
        Task<string> AddAsync(Certificate certificate, CancellationToken cancellationToken = default);
    }
}
