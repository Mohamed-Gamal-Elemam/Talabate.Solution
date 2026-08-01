using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity;
using Talabate.Core.IRepos;

namespace Talabate.Core.IUnitOfWork
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        IGenaricRepo<TEntity> Reposatory<TEntity>() where TEntity : BaseEntity;
        Task<int> CompleteAsync();


    }
}
