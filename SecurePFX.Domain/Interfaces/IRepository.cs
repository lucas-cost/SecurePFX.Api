using SecurePFX.Domain.Entities;

namespace SecurePFX.Domain.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task InsertAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, T entity, CancellationToken cancellationToken = default);
        Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
