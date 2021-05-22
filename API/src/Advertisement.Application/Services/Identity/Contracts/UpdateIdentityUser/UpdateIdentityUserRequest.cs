namespace Advertisement.Application.Services.Identity.Contracts.Update
{
    public class UpdateIdentityUserRequest
    {
        public string Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? NewPassword { get; set; }
        public string? CurrentPassword { get; set; }
        public string? Role { get; set; }
        public string? Phone { get; set; }
    }
}