using SecurePFX.Application.DTOs.Requests;
using SecurePFX.Application.DTOs.Responses;

namespace SecurePFX.Application.Interfaces
{
    public interface IAuthorizeCompanyService
    {
        Task<AuthorizeCompanyResponseDTO> AuthorizeCompanyAsync(AuthorizeCompanyRequestDTO authorizeCompanyRequestDTO);
    }
}
