using Advertisement.Domain.Shared;
using System;

namespace Advertisement.Application.Services.Petition.Contracts.Review
{
    public class ReviewPetitionRequest
    {
        public DecisionEnum DecisionEnum { get; set; }
        public string Text { get; set; }
        public Guid Id { get; set; }
    }
}
