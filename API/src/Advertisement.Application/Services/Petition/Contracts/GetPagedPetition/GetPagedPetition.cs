using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertisement.Application.Services.Petition.Contracts.GetPagedPetition
{
    public class GetPagedPetition
    {
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }

        public IEnumerable<GetPagedPetitionItem> Items { get; set; } = Enumerable.Empty<GetPagedPetitionItem>();
    }
}
