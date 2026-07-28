using Microsoft.EntityFrameworkCore;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.Specifications;
using Talabate.Repository.Genaric_Repoistory.Data;

namespace Talabate.Infrastructer.Genaric_Repoistory
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenaricRepo(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == typeof(Product))
            //{
            //    return (IEnumerable<T>) await _dbcontext.Set<Product>().Include(p => p.Category).Include(p => p.Brand).ToListAsync();
            //}
            return await _dbcontext.Set<T>().AsNoTracking().ToListAsync();
        }

       

        public async Task<T?> GetAsync(int id)
        {
            //if (typeof(T) == typeof(Product))
            //    return await _dbcontext.products.Include(p=>p.Category).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id) as T;
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T?> GetAWithSpecsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
           return await ApplySpecification(spec).CountAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>().AsQueryable(), spec);
        }

        
    }
}
