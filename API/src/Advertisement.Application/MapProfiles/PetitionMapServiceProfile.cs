using System;
using Advertisement.Application.Services.Petition.Contracts.Create;
using Advertisement.Application.Services.Petition.Contracts.GetPagedPetition;
using Advertisement.Domain;
using Advertisement.Domain.Shared;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class PetitionMapServiceProfile : Profile
    {
        public PetitionMapServiceProfile()
        {
            CreateMap<CreatePetitionRequest, Petition>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(x => x.Email))
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(x => x.Name))
                .ForMember(dest => dest.Text,
                    opt => opt.MapFrom(x => x.Text))
                .ForMember(dest => dest.Ad,
                    opt => opt.Ignore())
                .ForMember(dest => dest.AdId,
                    opt => opt.MapFrom(x => x.AdId))
                .ForMember(dest => dest.Reviewed,
                    opt => opt.Ignore())
                .ForMember(dest => dest.DecisionEnum,
                    opt => opt.MapFrom(x => DecisionEnum.Ok))
                .ForMember(dest => dest.ReviewResult,
                    opt => opt.Ignore());

            CreateMap<Petition, GetPagedPetitionItem>()
                .ForMember(dest => dest.Ad,
                    opt => opt.MapFrom(x => x.Ad));
            CreateMap<GetPagedPetitionRequest, GetPagedPetition>()
                .ForMember(dest => dest.Items,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Total,
                    opt => opt.Ignore());
        }
    }
}