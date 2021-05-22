using Advertisement.Application.Services.Petition.Contracts.Create;
using Advertisement.Application.Services.Petition.Contracts.GetPagedPetition;
using Advertisement.Application.Services.Petition.Contracts.Review;
using Advertisement.PublicApi.Models.Petition;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class AdPetitionControllerProfile : Profile
    {
        public AdPetitionControllerProfile()
        {
            CreateMap<CreatePetitionDto, CreatePetitionRequest>();
            CreateMap<ReviewPetitionDto, ReviewPetitionRequest>();
            CreateMap<GetPagedPetitionDto, GetPagedPetitionRequest>();
        }
    }
}