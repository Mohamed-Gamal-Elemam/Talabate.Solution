
using Talabate.Core.Entity;
using Talabate.Core.Specifications;

namespace Talabate.Core.IRepos
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAWithSpecsync(ISpecification<T> spec);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
    }
}
