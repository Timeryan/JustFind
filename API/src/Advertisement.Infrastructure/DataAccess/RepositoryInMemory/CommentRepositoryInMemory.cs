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
    public class CommentRepositoryInMemory : BaseRepositoryInMemory<Comment>, ICommentRepository
    {
        private static readonly ConcurrentDictionary<Guid, Comment> _repository = new();

        public CommentRepositoryInMemory() : base(_repository)
        {

        }

        public async Task<IEnumerable<Comment>> GetPaged(int offset, int limit,
            CancellationToken cancellationToken)
        {

            return _repository
                .Select(pair => pair.Value)
                .OrderBy(c => c.Id)
                .Skip(offset)
                .Take(limit);
        }
    }
}
