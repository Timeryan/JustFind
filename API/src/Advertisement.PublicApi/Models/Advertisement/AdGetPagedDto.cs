using System;


namespace Advertisement.PublicApi.Models.Advertisement
{
    public sealed class AdGetPagedDto
    {
        public string SearchName { get; set; } = "";
        public Guid SearchOwnerId { get; set; }
        public Guid SearchCategoryId { get; set; }
        public long? SearchLocationKladrId { get; set; }
        public string SortParam { get; set; } = "Date";
        public bool SortDirection { get; set; } = false;
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }
}
