using System;
using Advertisement.Application.Common;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.DeleteIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.Update;
using Advertisement.Application.Services.User.Contracts.DeleteDomainUser;
using Advertisement.Application.Services.User.Contracts.GetAllDomainUser;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Contracts.RegisterDomainUser;
using Advertisement.Application.Services.User.Contracts.UpdateDomainUser;
using Advertisement.Domain;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class UserMapServiceProfile : Profile
    {
        public UserMapServiceProfile()
        {
            CreateMap<RegisterDomainUserRequest, CreateIdentityUserRequest>()
                .ForMember(dest => dest.Role,
                    opt => opt.MapFrom(src => RoleConstants.UserRole))
                .ForMember(dest => dest.Login, 
                    opt => opt.MapFrom(src => src.Email));

            CreateMap<DeleteDomainUserRequest, DeleteIdentityUserRequest>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<User, GetAllDomainUserResponseItem>();

            CreateMap<RegisterDomainUserRequest, User>().ForMember(dest => dest.Photo, 
                opt=> opt.MapFrom(src => "")).ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => Guid.Empty))
                .ForMember(dest => dest.Active, opt => opt.MapFrom(x => true));

            CreateMap<CreateIdentityUserResponse, RegisterDomainUserResponse>().ForMember(dest => dest.UserId, 
                opt => opt.MapFrom(src => new Guid(src.UserId)));

            CreateMap<GetByIdDomainUserResponse, User>()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(x => x.Active));
            CreateMap<User, GetByIdDomainUserResponse>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<UpdateDomainUserRequest, UpdateIdentityUserRequest>().ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Login,
                    opt => opt.MapFrom(src => src.Email));

            CreateMap<UpdateDomainUserRequest, User>()
                .ForMember(dest => dest.Photo, opt =>opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore());
        }
    }
}