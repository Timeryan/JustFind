using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Domain;
using AutoMapper;

namespace Advertisement.Application.MapProfiles
{
    public class PhotoMapServiceProfile : Profile
    {
        public PhotoMapServiceProfile()
        {
            CreateMap<Photo, CreatePhotoResponse>();
        }
    }
}