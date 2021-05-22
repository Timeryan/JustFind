using System;

namespace Advertisement.Application.Services.Petition.Contracts.GetPagedPetition
{
    public sealed class GetPagedPetitionRequest
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }
}