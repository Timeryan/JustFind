using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Application.Services.Photo.Contracts.DeletePhoto;
using Advertisement.PublicApi.Models.Photo;
using AutoMapper;

namespace Advertisement.PublicApi.MapProfiles
{
    public class PhotoMapControllerProfile : Profile
    {
        public PhotoMapControllerProfile()
        {
            CreateMap<PhotoDeleteDto, DeletePhotoRequest>();
        }
    }
}