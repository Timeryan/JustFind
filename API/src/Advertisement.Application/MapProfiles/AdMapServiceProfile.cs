using System;
using System.Linq;
using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using Advertisement.Application.Services.Ad.Contracts.UpdateAd;
using Advertisement.Domain;
using Advertisement.Domain.Shared;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class AdMapServiceProfile : Profile
    {
        public AdMapServiceProfile()
        {
            CreateMap<CreateAdRequest, Ad>()
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => Statuses.OnSale))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Views,
                    opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.OwnerId,
                    opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Owner,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Category,
                    opt => opt.Ignore())
                .ForMember(dest => dest.LocationKladrId,
                    opt => opt.MapFrom(src => Convert.ToInt64(src.LocationKladrId.Substring(0,11))))
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ModerationComment,
                    opt => opt.Ignore());
            CreateMap<Ad, CreateAdResponse>();
            CreateMap<Ad, GetByIdAdResponse>()
                .ForMember(dest => dest.OwnerName,
                    opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.MiddleName}"))
                .ForMember(dest => dest.OwnerId,
                    opt => opt.MapFrom(src => src.Owner.Id))
                .ForMember(dest => dest.OwnerNumber,
                    opt => opt.Ignore())
                .ForMember(dest => dest.OwnerPhoto,
                    opt => opt.MapFrom(src => src.Owner.Photo))
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ModerationComment, 
                    opt => opt.MapFrom(x => x.ModerationComment))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(x => x.Status));
            CreateMap<UpdateAdRequest, Ad>()
                .ForMember(dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.OwnerId,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Owner,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Views,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Category,
                    opt => opt.Ignore())
                .ForMember(dest => dest.Photos,
                    opt => opt.Ignore())
                .ForMember(dest => dest.ModerationComment,
                    opt => opt.MapFrom(src=> string.Empty));
            CreateMap<GetPagedAdRequest, GetPagedAdResponse>()
                .ForMember(dest => dest.Total, 
                opt => opt.MapFrom(src => 0)) 
                .ForMember(dest => dest.Items, 
                opt => opt.Ignore());
            CreateMap<Ad, GetPagedAdResponseItem>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Photo,
                    opt => opt.MapFrom(src => src.Photos.FirstOrDefault().KodBase64))
                .ForMember(dest => dest.OwnerId,
                    opt => opt.MapFrom(src => src.OwnerId))
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.UpdatedAt));
        }
    }
}