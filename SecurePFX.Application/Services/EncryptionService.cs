using AutoMapper;
using Microsoft.Extensions.Configuration;
using SecurePFX.Application.Interfaces;
using System.Security.Cryptography;

namespace SecurePFX.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key; // 32 bytes for AES-256
        private readonly byte[] _iv; // 16 bytes for AES block size

        public EncryptionService(IConfiguration config)
        {
            _key = Convert.FromBase64String(config["CertificateVault:Encryption:Key"]!);
            _iv = Convert.FromBase64String(config["CertificateVault:Encryption:IV"]!);  
        }

        public byte[] Encrypt(byte[] plainData)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            return PerformCryptography(plainData, encryptor);
        }

        public byte[] Decrypt(byte[] cipherData)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = _iv;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            return PerformCryptography(cipherData, decryptor);
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using var ms = new MemoryStream();
            using var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return ms.ToArray();
        }
    }
}
