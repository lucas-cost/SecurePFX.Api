using Microsoft.EntityFrameworkCore;
using SecurePFX.Domain.Entities;
using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Infrastructure.Data.Contexts;

namespace SecurePFX.Infrastructure.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            return company; 
        }

        public async Task<bool> GetByCnpjAsync(string cnpj)
        {
            return await _context.Companies
                                 .AnyAsync(c => c.CNPJ == cnpj);
        }
    }
}
