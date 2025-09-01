using SecurePFX.Domain.Interfaces.Repositories;

namespace SecurePFX.Domain.Interfaces.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository Companies { get; }
        Task<int> CommitAsync();
        Task<int> RollbackAsync();
    }
}
