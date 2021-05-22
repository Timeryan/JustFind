using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.User.Contracts.DeleteDomainUser;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Contracts.RegisterDomainUser;
using Advertisement.Application.Services.User.Contracts.UpdateDomainUser;
using Advertisement.PublicApi.Models.User;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class UserMapControllerProfile : Profile
    {
        public UserMapControllerProfile()
        {
            CreateMap<UserRegisterDto, RegisterDomainUserRequest>();
            CreateMap<UserLoginDto, CreateTokenRequest>().ForMember(dest=> dest.Login, 
                opt=>opt.MapFrom(src=>src.Email));
            CreateMap<UserGetByIdDto, GetByIdDomainUserRequest>();
            CreateMap<UserUpdateDto, UpdateDomainUserRequest>().ForMember(dest => dest.Photo, opt =>opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.MapFrom(x => x.Active));
            CreateMap<UserDeleteDto, DeleteDomainUserRequest>();
        }
    }
}