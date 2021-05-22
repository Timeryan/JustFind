using System.ComponentModel.DataAnnotations;

namespace Advertisement.PublicApi.Models.User
{
    public sealed class UserRegisterDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Имя должно содержать менее 50 символов")]
        public string FirstName { get; set; }
        [MaxLength(50, ErrorMessage = "Отчество должно содержать менее 50 символов")]
        public string MiddleName { get; set; }
        [MaxLength(50, ErrorMessage = "Фамилия должне содержать менее 50 символов")]
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required] 
        public string ReCaptchaResponse { get; set; }
    }
}
