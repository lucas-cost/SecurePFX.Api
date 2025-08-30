using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Exceptions.Resource;
using SecurePFX.Application.Interfaces;
using SecurePFX.Application.Settings;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace SecurePFX.Application.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly CertificateSettings _settings;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public CertificateService(ICertificateRepository certificateRepository, IEncryptionService encryptionService, IMapper mapper, IOptions<CertificateSettings> settings)
        {
            _certificateRepository = certificateRepository;
            _encryptionService = encryptionService;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<CertificateResponseDTO> ProcessAndStoreCertificateAsync(CertificateUploadDTO certificateUploadDTO, CancellationToken cancellationToken)
        {
            ValidateFile(certificateUploadDTO.File);

            byte[] rawData = await ReadFileDataAsync(certificateUploadDTO.File);

            string password = GetCertificatePassword(certificateUploadDTO.Password!);
            X509Certificate2 cert = LoadCertificate(rawData, password);

            Certificate certificate = CreateCertificateEntity(certificateUploadDTO, cert, rawData);

            certificate.Id = await _certificateRepository.AddAsync(certificate, cancellationToken);

            CertificateResponseDTO response = _mapper.Map<CertificateResponseDTO>(certificate);

            return response;
        }

        private async Task<byte[]> ReadFileDataAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }

        private void ValidateFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ValidationException(SrcMsg.SRC001);

            if (!Path.GetExtension(file.FileName).Equals(".pfx", StringComparison.OrdinalIgnoreCase))
                throw new ValidationException(SrcMsg.SRC005);

            if (file.Length > _settings.MaxFileSizeInBytes)
                throw new ValidationException(SrcMsg.SRC006);
        }

        private Certificate CreateCertificateEntity(CertificateUploadDTO dto, X509Certificate2 cert, byte[] rawData)
        {
            Certificate certificate = _mapper.Map<Certificate>(dto);
            _mapper.Map(cert, certificate);
            certificate.RawData = _encryptionService.Encrypt(rawData);

            return certificate;
        }

        private string GetCertificatePassword(string providedPassword)
        {
            return string.IsNullOrWhiteSpace(providedPassword) ? _settings.DefaultPassword : providedPassword;
        }

        private X509Certificate2 LoadCertificate(byte[] rawData, string password)
        {
            try
            {
                return new X509Certificate2(
                    rawData,
                    password,
                    X509KeyStorageFlags.EphemeralKeySet
                );
            }
            catch (CryptographicException ex)
            {
                throw new ValidationException(SrcMsg.SRC007, ex);
            }
        }
    }
}
