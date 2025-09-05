using SecurePFX.Domain.Entities;

namespace SecurePFX.Domain.Interfaces.Repositories
{
    public interface IAuthorizeCompanyRepository
    {
        Task<AuthorizeCompany> CreateAsync(AuthorizeCompany authorizeCompany);
        Task<AuthorizeCompany?> GetAuthorizedCompany(string cnpj, Guid code);
        Task<bool> GetCompanyByCnpjAndCodeAsync(string cnpj, Guid code);
        Task<bool> GetCompanyByCnpjAsync(string cnpj);
        Task<AuthorizeCompany> UpdateAsync(int id, int? companyId);
    }
}
