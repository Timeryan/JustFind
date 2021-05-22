using Advertisement.Application.Repository;
using Advertisement.Domain;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class CategoryRepositoryInDataBase : BaseRepositoryInDataBase<Category> ,ICategoryRepository
    {
        public CategoryRepositoryInDataBase(DataBaseContext dbContext) : base(dbContext)
        {
        }
    }
}