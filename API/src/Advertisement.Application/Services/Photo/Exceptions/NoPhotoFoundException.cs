using System;
using Advertisement.Domain.Shared.Exceptions;

namespace Advertisement.Application.Services.Photo.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при работе с фотографиями.
    /// </summary>
    public class NoPhotoFoundException : NotFoundException
    {
        /// <summary>
        /// Имя сущности.
        /// </summary>
        private static string entityName = "Фото";
        public NoPhotoFoundException(Guid Id) : base(entityName, Id.ToString())
        {
        }
    }
}