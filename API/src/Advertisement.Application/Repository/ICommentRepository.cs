using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Domain;

namespace Advertisement.Application.Repository
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
    }
}