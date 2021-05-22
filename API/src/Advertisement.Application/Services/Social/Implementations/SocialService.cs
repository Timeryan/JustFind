using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.Social.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.Application.Services.Social.Interfaces
{
    public class SocialService : ISocialService
    {
        private readonly IIdentityService _identityService;

        public SocialService(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<CreateTokenResponse> SignIn(SocialAuthenticationRequest request, CancellationToken cancellationToken)
        {
            return await _identityService.SocialSignIn(request, cancellationToken);
        }
    }
}