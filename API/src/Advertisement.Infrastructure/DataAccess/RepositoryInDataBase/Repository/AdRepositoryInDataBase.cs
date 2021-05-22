using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class AdRepositoryInDataBase : BaseRepositoryInDataBase<Ad>, IAdRepository
    {
        public AdRepositoryInDataBase(DataBaseContext context) : base(context)
        {
        }
    }
}