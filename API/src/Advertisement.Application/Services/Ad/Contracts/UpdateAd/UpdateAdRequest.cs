using System;
using System.Collections.Generic;
using Advertisement.Domain;
using Advertisement.Domain.Shared;

namespace Advertisement.Application.Services.Ad.Contracts.UpdateAd
{
    public class UpdateAdRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Text { get; set; }
        public ICollection<Guid> Photos { get; set; }
        public decimal? Price { get; set; }
        public Guid? CategoryId { get; set; }
        
        public long LocationKladrId { get; set; }
        public string LocationText { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
        public Statuses Status { get; set; }
    }
}