using Advertisement.Application.Services.Petition.Contracts.Create;
using Advertisement.Application.Services.Petition.Contracts.GetPagedPetition;
using Advertisement.Application.Services.Petition.Contracts.Review;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.Application.Services.Petition.Interface
{
    public interface IPetitionService
    {
        Task CreatePetition(CreatePetitionRequest createPetitionRequest, CancellationToken cancellationToken);
        Task ReviewPetition(ReviewPetitionRequest reviewPetitionRequest, CancellationToken cancellationToken);
        Task<GetPagedPetition> GetPaged(GetPagedPetitionRequest request, CancellationToken cancellationToken);
    }
}