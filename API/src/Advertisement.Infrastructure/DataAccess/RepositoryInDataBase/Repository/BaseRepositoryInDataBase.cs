using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class BaseRepositoryInDataBase<TEntity>
        : IBaseRepository<TEntity> where TEntity : Entity
    {
        private readonly DataBaseContext _dbContext;
        public BaseRepositoryInDataBase(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<TEntity> FindById(Guid? id, CancellationToken cancellationToken)
        {
            return await _dbContext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var data = _dbContext.Set<TEntity>();
            var findData = data.Where(predicate);
            return await findData.ToListAsync();
        }

        public async Task Save(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                await _dbContext.AddAsync(entity, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteById(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.FindAsync<TEntity>(new object[] {id}, cancellationToken: cancellationToken);
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken)
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<int> Count(CancellationToken cancellationToken)
        {
            var data = _dbContext.Set<TEntity>();

            return await data.CountAsync(cancellationToken);
        }
    }
}