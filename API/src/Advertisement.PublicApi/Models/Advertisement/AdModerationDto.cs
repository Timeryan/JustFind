using System;

namespace Advertisement.PublicApi.Models.Advertisement
{
    public class AdModerationDto 
    { 
        public Guid AdId { get; set; }
        public bool Approved { get; set; }
        public string Comment { get; set; }
    }
}