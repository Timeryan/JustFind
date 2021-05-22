using Advertisement.Domain.Shared;
using System;

namespace Advertisement.PublicApi.Models.Petition
{
    public class ReviewPetitionDto
    {
        public DecisionEnum DecisionEnum { get; set; }
        public string Text { get; set; }
        public Guid Id { get; set; }
    }
}
