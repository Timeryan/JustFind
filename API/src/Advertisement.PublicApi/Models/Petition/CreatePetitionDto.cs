using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advertisement.PublicApi.Models.Petition
{
    public class CreatePetitionDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public Guid AdId { get; set; }
    }
}
