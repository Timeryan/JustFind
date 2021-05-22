using System;

namespace Advertisement.Application.Services.Ad.Contracts.GetById
{
    public class GetByIdAdPhotoItem
    {
        public Guid Id { get; set; }
        public string KodBase64 { get; set; }
    }
}