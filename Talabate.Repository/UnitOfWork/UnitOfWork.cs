using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;
using Talabate.Core.IUnitOfWork;
using Talabate.Infrastructer.Genaric_Repoistory;
using Talabate.Repository.Genaric_Repoistory.Data;

namespace Talabate.Infrastructer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbcontext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
            _repositories = new Hashtable();
        }


        public IGenaricRepo<TEntity> Reposatory<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repo =new GenaricRepo<TEntity>(_dbcontext);
                _repositories.Add(key, repo);
            }
            return _repositories[key] as IGenaricRepo<TEntity>;
        }
        public Task<int> CompleteAsync()
        => _dbcontext.SaveChangesAsync();

        public ValueTask DisposeAsync()
        => _dbcontext.DisposeAsync();

       
    }
}
