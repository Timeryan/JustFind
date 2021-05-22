using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Photo.Exceptions
{
    public class OverflowMaxSizePhotoException : EntityNotInValidStateException
    {
        private static string entityName = "Фото";
        
        public OverflowMaxSizePhotoException() : base(entityName, "Слишком большой размер фотографии")
        {
            
        }
    }
}