using System;
using System.Threading.Tasks;
using HLCardGame.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace HLCardGame.Infrastructure.Repositories
{
    public abstract class GenericRepository<TEntity, Domain> : IGenericRepository<TEntity, Domain>
        where TEntity : class where Domain : class
    {
        protected readonly HLCardGameDbContext DbContext;

        protected readonly DbSet<TEntity> _table = null;

        public delegate Domain ToDomainDelegate(TEntity entity);

        public delegate TEntity ToEntityDelegate(Domain domain);

        private readonly ToDomainDelegate _toDomainDelegate;
        private readonly ToEntityDelegate _toEntityDelegate;

        public GenericRepository(
            HLCardGameDbContext dbContext,
            ToEntityDelegate toEntityDelegate,
            ToDomainDelegate toDomainDelegate)
        {
            DbContext = dbContext ??
              throw new ArgumentNullException(nameof(dbContext));

            _table = DbContext.Set<TEntity>();

            _toEntityDelegate = toEntityDelegate;
            _toDomainDelegate = toDomainDelegate;
        }

        public async Task CreateAsync(Domain domain)
        {
            var entity = _toEntityDelegate(domain);
            await _table.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Domain> GetByIdAsync(object id)
        {
            return _toDomainDelegate(await _table.FindAsync(id));
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var record = await _table.FindAsync(id);
            _table.Remove(record);
            return await DbContext.SaveChangesAsync();
        }
    }
}