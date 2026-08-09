
using Talabate.Core.Entity;
using Talabate.Core.Specifications;

namespace Talabate.Core.IRepos
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetWithSpecsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task AddAsync (T entity);
        void UpdateAsync (T entity);
        void DeleteAsync (T entity);
        
    }
}
