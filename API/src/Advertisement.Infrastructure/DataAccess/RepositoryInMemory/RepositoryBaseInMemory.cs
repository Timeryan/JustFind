using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain.Shared;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInMemory
{
    public class BaseRepositoryInMemory<TEntity>
        : IBaseRepository<TEntity> where TEntity : Entity

    { 
        private readonly ConcurrentDictionary<Guid, TEntity> _repository = new();

        public BaseRepositoryInMemory(ConcurrentDictionary<Guid, TEntity> repository)
        {
            _repository = repository;
        }
        

        public async Task<TEntity> FindById(Guid? id, CancellationToken cancellationToken)
    {
        //_repository.TryGetValue(id, out var entity);
        //return entity;
        throw new NotImplementedException();

    }

    public async Task<IEnumerable<TEntity>> FindWhere(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var compiled = predicate.Compile();
        return _repository.Select(pair => pair.Value).Where(compiled);
    }

    public async Task Save(TEntity entity, CancellationToken cancellationToken)
    {
        
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }
        

        _repository.AddOrUpdate(entity.Id, (e) => entity, (i, user) => entity);
        
    }

    public async Task DeleteById(Guid id, CancellationToken cancellationToken)
    {
        _repository.TryRemove(id, out var entity);
    }

    public async Task<IEnumerable<TEntity>> Get(CancellationToken cancellationToken)
    {
        return _repository
            .Select(pair => pair.Value)
            .OrderBy(u => u.Id);
    }

    public async Task<int> Count(CancellationToken cancellationToken)
    {
        return _repository.Count();
    }
    }
}