using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;

namespace SecurePFX.Application.Interfaces
{
    public interface ICertificateService
    {
        Task<CertificateResponseDTO> ProcessAndStoreCertificateAsync(CertificateUploadDTO uploadCertificateDTO, CancellationToken cancellationToken);
    }
}
