using System;

namespace Advertisement.Application.Services.Petition.Contracts.Create
{
    public class CreatePetitionRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public Guid AdId { get; set; }
    }
}