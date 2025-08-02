namespace SecurePFX.Infrastructure.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string CertificateCollectionName { get; set; } = string.Empty;
    }
}
