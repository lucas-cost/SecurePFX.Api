using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;

namespace SecurePFX.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<CompanyResponseDTO> CreateCompanyAsync(CreateCompanyRequestDTO dto);
    }
}
