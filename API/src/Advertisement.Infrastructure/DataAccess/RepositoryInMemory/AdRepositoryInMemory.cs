using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess.RepositoryInMemory;

namespace Advertisement.Infrastructure.DataAccess
{
    public class AdBaseRepositoryInMemory : BaseRepositoryInMemory<Ad>, IAdRepository
    {
        private static readonly ConcurrentDictionary<Guid, Ad> _repository = new();

        public AdBaseRepositoryInMemory():base(_repository)
        {
            
        }

        public async Task<IEnumerable<Ad>> GetPaged(int offset, int limit,
            CancellationToken cancellationToken)
        {
            
            return _repository
                .Select(pair => pair.Value)
                .OrderBy(u => u.Id)
                .Skip(offset)
                .Take(limit);
        }
    }
}