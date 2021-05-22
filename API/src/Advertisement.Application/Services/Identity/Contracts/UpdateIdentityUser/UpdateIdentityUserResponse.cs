namespace Advertisement.Application.Services.Identity.Contracts.Update
{
    public class UpdateIdentityUserResponse
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}