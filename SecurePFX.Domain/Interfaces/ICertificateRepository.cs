using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Enums;

namespace SecurePFX.Domain.Interfaces
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<Certificate?> GetActiveAsync(CertificateContext context, CertificateCategory category, CancellationToken cancellationToken = default);
    }
}
