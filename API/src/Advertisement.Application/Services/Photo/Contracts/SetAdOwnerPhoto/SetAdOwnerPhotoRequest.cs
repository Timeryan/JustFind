using System;

namespace Advertisement.Application.Services.Photo.Contracts.SetAdOwnerPhoto
{
    public class SetAdOwnerPhotoRequest
    {
        public Guid Id { get; set; }
        public Guid AdOwnerId { get; set; }
    }
}