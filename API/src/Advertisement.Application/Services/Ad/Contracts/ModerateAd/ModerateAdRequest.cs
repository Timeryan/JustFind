using System;

namespace Advertisement.Application.Services.Ad.Contracts.ModerateAd
{
    public sealed class ModerateAdRequest
    {
        public Guid AdId { get; set; }
        public bool Approved { get; set; }
        public string Comment { get; set; }
    }
}