using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertisement.PublicApi.Models.Petition
{
    public class GetPagedPetitionDto
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}
