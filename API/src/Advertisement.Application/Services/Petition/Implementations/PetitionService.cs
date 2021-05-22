using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Comment.Exceptions;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.Petition.Contracts.Create;
using Advertisement.Application.Services.Petition.Contracts.GetPagedPetition;
using Advertisement.Application.Services.Petition.Contracts.Review;
using Advertisement.Application.Services.Petition.Interface;
using Advertisement.Domain.Shared;
using Advertisement.Domain.Shared.Exceptions;
using AutoMapper;

namespace Advertisement.Application.Services.Petition.Implementations
{
    public sealed class PetitionService : IPetitionService
    {
        private readonly IMapper _mapper;
        private readonly IPetitionRepository _petitionRepository;
        private readonly IAdService _adService;
        private readonly IIdentityService _identityService;

        public PetitionService(IMapper mapper, IPetitionRepository petitionRepository, IAdService adService, IIdentityService identityService)
        {
            _petitionRepository = petitionRepository;
            _mapper = mapper;
            _adService = adService;
            _identityService = identityService;
        }

        public async Task CreatePetition(CreatePetitionRequest createPetitionRequest, CancellationToken cancellationToken)
        {
            var petition = _mapper.Map<Domain.Petition>(createPetitionRequest);
            petition.Id = Guid.NewGuid();
            await _petitionRepository.Save(petition, cancellationToken);
        }

        public async Task<GetPagedPetition> GetPaged(GetPagedPetitionRequest request, CancellationToken cancellationToken)
        {
            if (!await _identityService.CurrentUserAdmin(cancellationToken))
                throw new NoRightsException("Доступ к жалобам имеет только администратор.");

            var total = await _petitionRepository.Count(cancellationToken);
            var result = _mapper.Map<GetPagedPetition>(request);
            if (total == 0)
            {
                return result;
            }

            var petitions = await _petitionRepository.FindWhere(x => !x.Reviewed, cancellationToken);
            result.Items = _mapper.Map<GetPagedPetitionItem[]>(petitions.Skip(request.Offset).Take(request.Limit));
            result.Total = total;

            return result;
        }

        public async Task ReviewPetition(ReviewPetitionRequest reviewPetitionRequest, CancellationToken cancellationToken)
        {
            var petition = await _petitionRepository.FindById(reviewPetitionRequest.Id, cancellationToken);
            if (petition == null)
                throw new PetitionNotFoundException(reviewPetitionRequest.Id);

            petition.Reviewed = true;
            petition.DecisionEnum = reviewPetitionRequest.DecisionEnum;
            petition.ReviewResult = reviewPetitionRequest.Text;

            switch (petition.DecisionEnum)
            {
                case DecisionEnum.BanAd:
                    var petitionsToResole = await _petitionRepository
                        .FindWhere(x => x.AdId == petition.AdId && !x.Reviewed, cancellationToken);
                    foreach(var pet in petitionsToResole)
                    {
                        pet.Reviewed = true;
                        pet.DecisionEnum = reviewPetitionRequest.DecisionEnum;
                        pet.ReviewResult = reviewPetitionRequest.Text;

                        await _petitionRepository.Save(pet, cancellationToken);
                    }

                    await _adService.UpdateAdStatus(petition.AdId, Statuses.Rejected, petition.ReviewResult, cancellationToken);
                    break;

                case DecisionEnum.BanUser:
                    var petitions = await _petitionRepository
                        .FindWhere(x => x.AdId == petition.AdId && !x.Reviewed, cancellationToken);
                    foreach (var pet in petitions)
                    {
                        pet.Reviewed = true;
                        pet.DecisionEnum = reviewPetitionRequest.DecisionEnum;
                        pet.ReviewResult = reviewPetitionRequest.Text;

                        await _petitionRepository.Save(pet, cancellationToken);
                    }

                    await _adService.RejectAllAdsOfUser(petition.Ad.OwnerId, petition.ReviewResult, cancellationToken);
                    break;
                default:
                    break;
            }

            await _petitionRepository.Save(petition, cancellationToken);
        }
    }
}