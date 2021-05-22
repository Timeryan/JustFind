using System;

namespace Advertisement.Application.Services.User.Contracts.UpdateDomainUser
{
    public class UpdatePhotoDomainUserRequest
    {
        public Guid Id { get; set; }
        public byte[] Photo { get; set; }
    }
}