﻿namespace Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser
{
    public class CreateIdentityUserRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        
    }
}