using SecurePFX.Domain.Enums;

namespace SecurePFX.Application.DTOs.Responses
{
    public class CertificateResponseDTO
    {
        public string Id { get; set; } = default!;
        public CertificateContext Context { get; set; }
        public CertificateCategory Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
