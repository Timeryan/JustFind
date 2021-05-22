using Advertisement.Application.Repository;
using Advertisement.Application.Services.Photo.Interface;
using Advertisement.Domain;

namespace Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository
{
    public class PetitionRepositoryInDataBase : BaseRepositoryInDataBase<Petition>, IPetitionRepository
    {
        public PetitionRepositoryInDataBase(DataBaseContext dbContext) : base(dbContext)
        {
        }
    }
}