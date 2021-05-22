namespace Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser
{
    public class CreateIdentityUserResponse
    {
        public bool IsSuccess { get; set; }
        public string UserId { get; set; }
        public string[] Errors { get; set; }
    }
}
