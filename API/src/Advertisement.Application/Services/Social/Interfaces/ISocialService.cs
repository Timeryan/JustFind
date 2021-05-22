using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.Social.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.Application.Services.Social.Interfaces
{
    public interface ISocialService
    {
        Task<CreateTokenResponse> SignIn(SocialAuthenticationRequest request, CancellationToken cancellationToken);
    }
}