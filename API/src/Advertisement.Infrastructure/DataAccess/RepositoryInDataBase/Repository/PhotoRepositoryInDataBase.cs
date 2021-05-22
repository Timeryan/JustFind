using Advertisement.Application.Repository;
using Advertisement.Application.Services.Photo.Interface;
using Advertisement.Domain;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class PhotoRepositoryInDataBase : BaseRepositoryInDataBase<Photo>,IPhotoRepository
    {
        public PhotoRepositoryInDataBase(DataBaseContext dbContext) : base(dbContext)
        {
        }
    }
}