using System;
using System.ComponentModel.DataAnnotations;


namespace Advertisement.PublicApi.Models.User
{
    public sealed class UserLoginDto
    {
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
