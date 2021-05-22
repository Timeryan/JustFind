namespace Advertisement.Application.Services.Social.Contracts
{
    public class SocialAuthenticationRequest
    {
        public string Provider { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string AuthToken { get; set; }
    }
}
