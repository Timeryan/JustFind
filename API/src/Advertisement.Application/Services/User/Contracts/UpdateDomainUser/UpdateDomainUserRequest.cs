using System;

namespace Advertisement.Application.Services.User.Contracts.UpdateDomainUser
{
    public class UpdateDomainUserRequest
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public byte[]? Photo { get; set; }
        public string? Role { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public bool Active { get; set; }
    }
}