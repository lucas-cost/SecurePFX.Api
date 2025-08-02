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

        public CertificateContext Context { get; set; }

        public CertificateCategory Category { get; set; }

        public bool IsActive { get; set; }

        [BsonIgnoreIfNull]
        public string? Password { get; set; } // Opcional: criptografar com AES

        public DateTime StorageDate { get; set; }

        public DateTime? ExpirationDate { get; set; }
    }
}
