using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Application.Services.Petition.Contracts.GetPagedPetition
{
    public class GetPagedPetitionItem
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Guid Id { get; set; }
        public Guid AdId { get; set; }
        public GetPagedAdResponseItem Ad { get; set; }
    }
}
