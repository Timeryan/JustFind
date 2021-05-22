using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess.RepositoryInMemory;

namespace Advertisement.Infrastructure.DataAccess
{
    public class UserBaseRepositoryInMemory : BaseRepositoryInMemory<User>,IUserRepository
    { 
        private static readonly ConcurrentDictionary<Guid, User> _repository = new();

        public UserBaseRepositoryInMemory() : base(_repository)
        {
        }

    }
}