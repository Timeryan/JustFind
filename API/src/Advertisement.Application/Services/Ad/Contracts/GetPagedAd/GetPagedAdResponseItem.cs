using System;

namespace Advertisement.Application.Services.Ad.Contracts.GetPagedAd
{
    public sealed class GetPagedAdResponseItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public string LocationText { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}