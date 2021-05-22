using System;

namespace Advertisement.Application.Services.Ad.Contracts.GetPagedAd
{
    public sealed class GetPagedAdRequest
    {
        public string SearchName { get; set; }
        public Guid SearchOwnerId { get; set; }
        public Guid SearchCategoryId { get; set; } 
        public long? SearchLocationKladrId { get; set; } 
        public string SortParam { get; set; } 
        public bool SortDirection { get; set; } 
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}