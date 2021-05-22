using System;
using System.Collections.Generic;
using Advertisement.Domain;
using Advertisement.Domain.Shared;

namespace Advertisement.Application.Services.Ad.Contracts.GetById
{
    public sealed class GetByIdAdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public ICollection<GetByIdAdPhotoItem> Photos { get; set; }
        public decimal Price { get; set; }
        public string OwnerName { get; set; }
        public string OwnerNumber { get; set; }
        public string OwnerPhoto { get; set; }
        public string LocationText { get; set; }
        public string Status { get; set; }
        public string ModerationComment { get; set; }
        public Guid OwnerId { get; set; }
    }
}