using Advertisement.Application.Repository;
using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class UserRepositoryInDataBase : BaseRepositoryInDataBase<User>, IUserRepository
    {
        public UserRepositoryInDataBase(DataBaseContext context) : base(context)
        {
            
        }
    }
}