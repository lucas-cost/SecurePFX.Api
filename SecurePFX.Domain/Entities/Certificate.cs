namespace SecurePFX.Domain.Entities
{
    public class Certificate : EntityBase
    {
        public byte[] RawData { get; set; } = default!;
        public CertificateMetadata Metadata { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
