using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;
using SecurePFX.Application.Interfaces;
using SecurePFX.Application.Settings;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SecurePFX.Application.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly CertificateSettings _settings;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMapper _mapper;

        public CertificateService(ICertificateRepository certificateRepository, IMapper mapper, IOptions<CertificateSettings> settings)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _settings = settings.Value;
        }

        public async Task<CertificateResponseDTO> ProcessAndStoreCertificateAsync(CertificateUploadDTO uploadCertificateDTO, CancellationToken cancellationToken)
        {
            // Validações
            ValidateFile(uploadCertificateDTO.File);

            // Processamento
            Certificate certificate = _mapper.Map<Certificate>(uploadCertificateDTO);
            certificate.RawData = await ReadFileDataAsync(uploadCertificateDTO.File);
            certificate.StorageDate = DateTime.UtcNow;

            // Persistência
            var certificateId = await _certificateRepository.AddAsync(certificate, cancellationToken);

            var response = _mapper.Map<CertificateResponseDTO>(certificate);

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
                throw new ValidationException("O arquivo do certificado é obrigatório.");

            if (!Path.GetExtension(file.FileName).Equals(".pfx", StringComparison.OrdinalIgnoreCase))
                throw new ValidationException("Apenas arquivos .pfx são aceitos.");

            if (file.Length > _settings.MaxFileSizeInBytes)
                throw new ValidationException("Tamanho máximo do arquivo: 5MB.");
        }
    }
}
