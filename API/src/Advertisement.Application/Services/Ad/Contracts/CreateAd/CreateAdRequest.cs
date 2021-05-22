using System;
using System.Collections.Generic;
using Advertisement.Domain;

namespace Advertisement.Application.Services.Ad.Contracts.CreateAd
{
    public sealed class CreateAdRequest
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public ICollection<Guid> Photos { get; set; }
        public decimal Price { get; set; }
        public Guid? CategoryId { get; set; }
        
        public string LocationKladrId { get; set; }
        public string LocationText { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
            
    }
}