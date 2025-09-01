using SecurePFX.Domain.Entities;

namespace SecurePFX.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> CreateAsync(Company company);
        Task<Company> GetByCnpjAsync(string cnpj);
    }
}
