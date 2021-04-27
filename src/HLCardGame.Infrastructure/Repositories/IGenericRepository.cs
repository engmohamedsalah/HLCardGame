using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HLCardGame.Infrastructure.Repositories
{
    public interface IGenericRepository<TEntity, Domain> where TEntity : class where Domain : class
    {
        Task CreateAsync(Domain entity);

        Task<Domain> GetByIdAsync(object id);

        Task<int> DeleteAsync(Guid id);
    }
}