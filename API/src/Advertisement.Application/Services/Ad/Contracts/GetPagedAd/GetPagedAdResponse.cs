using System.Collections.Generic;
using System.Linq;

namespace Advertisement.Application.Services.Ad.Contracts.GetPagedAd
{
    public sealed class GetPagedAdResponse
    {
        public int Total { get; set; }
        public int Offset { get; set; } 
        public int Limit { get; set; }

        public IEnumerable<GetPagedAdResponseItem> Items { get; set; } = Enumerable.Empty<GetPagedAdResponseItem>();
    }
}