namespace Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken
{
    public class CreateTokenRequest
    {
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
    }
}