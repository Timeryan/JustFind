using System;

namespace Advertisement.Application.Services.User.Contracts.GetByIdDomainUser
{
    public class GetByIdDomainUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
    }
}