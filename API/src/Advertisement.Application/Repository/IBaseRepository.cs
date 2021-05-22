using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.Application.Repository
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> FindById(Guid? id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task Save(TEntity entity, CancellationToken cancellationToken);
        Task DeleteById(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken);
        Task<int> Count(CancellationToken cancellationToken);
    }
}
