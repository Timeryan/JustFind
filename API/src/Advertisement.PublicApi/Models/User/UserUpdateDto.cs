using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace Advertisement.PublicApi.Models.User
{
    public sealed class UserUpdateDto
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Required]
        public Guid Id { get; set; }
        public string CurrentPassword { get; set; }
        [MinLength(6)]
        public string NewPassword { get; set; }
        public string Role { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
    }
}