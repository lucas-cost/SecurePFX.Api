using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SecurePFX.Domain.Enums;

namespace SecurePFX.Domain.Entities
{
    public class Certificate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = default!;

        [BsonElement("file_data")]
        public byte[] RawData { get; set; } = default!;

        public string Thumbprint { get; set; }

        public string Subject { get; set; }

        public string Issuer { get; set; }

        public DateTime NotBefore { get; set; }

        public DateTime NotAfter { get; set; }

        public CertificateContext Context { get; set; }

        public CertificateCategory Category { get; set; }

        public bool IsActive { get; set; }

        public DateTime StorageDate { get; set; }
    }
}
