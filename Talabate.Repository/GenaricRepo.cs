using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.Specifications;
using Talabate.Infrastructer;
using Talabate.Repository.Data;

namespace Talabate.Repository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenaricRepo(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
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

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<T?> GetAWithSpecsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }


        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbcontext.Set<T>().AsQueryable(), spec);
        }
    }
}
