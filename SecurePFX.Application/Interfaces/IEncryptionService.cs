namespace SecurePFX.Application.Interfaces
{
    public interface IEncryptionService
    {
        byte[] Encrypt(byte[] plainData);
        byte[] Decrypt(byte[] cipherData);
    }
}
