using MassTransit.Internals.GraphValidation;
using Microsoft.EntityFrameworkCore;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Infrastructure.Data.Contexts;

namespace SecurePFX.Infrastructure.Data.Repositories
{
    public class AuthorizeCompanyRepository : IAuthorizeCompanyRepository
    {
        private readonly AppDbContext _context;

        public AuthorizeCompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorizeCompany> CreateAsync(AuthorizeCompany authorizeCompany)
        {
            await _context.AuthorizedCompanies.AddAsync(authorizeCompany);
            return authorizeCompany;
        }

        public async Task<bool> GetCompanyByCnpjAsync(string cnpj)
        {
            return await _context.AuthorizedCompanies
                .AnyAsync(c => c.CompanyCnpj == cnpj);
        }

        public async Task<bool> GetCompanyByCnpjAndCodeAsync(string cnpj, Guid code)
        {
            return await _context.AuthorizedCompanies
                .AnyAsync(ac => ac.CompanyCnpj == cnpj && ac.CompanyCode == code);
        }
    }
}
