using SecurePFX.Domain.Enums;

namespace SecurePFX.Domain.Entities
{
    public class CertificateMetadata
    {
        public CertificateContext Context { get; set; }
        public CertificateCategory Category { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public string Subject { get; set; } = string.Empty;
    }
}
