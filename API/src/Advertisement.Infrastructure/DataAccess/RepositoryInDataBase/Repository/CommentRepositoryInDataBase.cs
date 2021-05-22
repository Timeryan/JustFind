using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Domain;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class CommentRepositoryInDataBase : BaseRepositoryInDataBase<Comment>,ICommentRepository
    {
        public CommentRepositoryInDataBase(DataBaseContext context) : base(context)
        {
            
        }
    }
}