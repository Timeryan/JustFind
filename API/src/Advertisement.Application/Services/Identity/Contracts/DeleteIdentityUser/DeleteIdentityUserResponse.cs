namespace Advertisement.Application.Services.Identity.Contracts.DeleteIdentityUser
{
    public class DeleteIdentityUserResponse
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}