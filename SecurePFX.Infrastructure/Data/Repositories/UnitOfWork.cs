using SecurePFX.Domain.Interfaces.Repositories;
using SecurePFX.Domain.Interfaces.UoW;
using SecurePFX.Infrastructure.Data.Contexts;

namespace SecurePFX.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICompanyRepository _companyRepository;
        private IAuthorizeCompanyRepository _authorizeRepository;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICompanyRepository Companies
            => _companyRepository ??= new CompanyRepository(_context);

        public IAuthorizeCompanyRepository AuthorizedCompanies
            => _authorizeRepository ??= new AuthorizeCompanyRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<int> RollbackAsync()
        {
            _context.ChangeTracker.Clear();

            return Task.FromResult(0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
