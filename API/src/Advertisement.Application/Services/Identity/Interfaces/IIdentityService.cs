using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.Identity.Contracts.DeleteIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.GetCurrentIdentityUserId;
using Advertisement.Application.Services.Identity.Contracts.IsInIdentityUserRole;
using Advertisement.Application.Services.Identity.Contracts.Update;
using Advertisement.Application.Services.Social.Contracts;

namespace Advertisement.Application.Services.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<GetCurrentIdentityUserIdResponse> GetCurrentIdentityUserId(CancellationToken cancellationToken = default);
        Task<IsInIdentityUserRoleResponse> IsInIdentityUserRole(IsInIdentityUserRoleRequest request, CancellationToken cancellationToken = default);
        Task ConfirmEmail(string userId, string code, CancellationToken cancellationToken =default);
        Task<CreateIdentityUserResponse> CreateIdentityUser(CreateIdentityUserRequest request, CancellationToken cancellationToken = default);
        Task<CreateTokenResponse> CreateIdentityUserToken(CreateTokenRequest request, CancellationToken cancellationToken = default);
        Task<UpdateIdentityUserResponse> UpdateIdentityUser(UpdateIdentityUserRequest request, CancellationToken cancellationToken);
        Task<DeleteIdentityUserResponse> DeleteIdentityUser(DeleteIdentityUserRequest request, CancellationToken cancellationToken);
        Task<bool> CurrentUserAdmin(CancellationToken cancellationToken);
        Task<string> GetIdentityUserRole(string id);
        Task<CreateTokenResponse> SocialSignIn(SocialAuthenticationRequest request, CancellationToken cancellationToken);
    }
}
