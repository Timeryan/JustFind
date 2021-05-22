namespace Advertisement.Application.Services.User.Contracts.RegisterDomainUser
{
    public sealed class RegisterDomainUserRequest
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        
        public string ReCaptchaResponse { get; set; }
            
    }
}