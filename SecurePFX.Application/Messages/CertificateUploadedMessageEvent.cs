using SecurePFX.Domain.Enums;

namespace SecurePFX.Application.Messages
{
    public sealed record CertificateUploadedMessageEvent
    {
        public string CertificateId { get; init; }
        public CertificateContext Context { get; set; }
        public CertificateCategory Category { get; set; }
        public bool? IsActive { get; init; }
        public DateTime LastUpdated { get; init; }
    }
}
