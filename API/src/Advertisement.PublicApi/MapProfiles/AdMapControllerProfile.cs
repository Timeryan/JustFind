using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Ad.Contracts.DeleteAd;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using Advertisement.Application.Services.Ad.Contracts.ModerateAd;
using Advertisement.Application.Services.Ad.Contracts.UpdateAd;
using Advertisement.PublicApi.Models.Advertisement;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class AdMapControllerProfile : Profile
    {
        public AdMapControllerProfile()
        {
            CreateMap<AdCreateDto, CreateAdRequest>();
            CreateMap<AdGetPagedDto, GetPagedAdRequest>();
            CreateMap<AdGetByIdDto, GetByIdAdRequest>();
            CreateMap<AdUpdateDto, UpdateAdRequest>()
                .ForMember(dest => dest.Status,
                opt => opt.Ignore());
            CreateMap<AdDeleteDto, DeleteAdRequest>();
            CreateMap<AdModerationDto, ModerateAdRequest>();
        }
        
    }
}