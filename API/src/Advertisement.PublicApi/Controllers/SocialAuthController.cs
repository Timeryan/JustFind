using Advertisement.Application.Services.Social.Contracts;
using Advertisement.Application.Services.Social.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Advertisement.PublicApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("social")]
    public class SocialAuthController : ControllerBase
    {
        private readonly ISocialService _socialService;

        public SocialAuthController(ISocialService socialService)
        {
            _socialService = socialService;
        }

        [HttpPost("external-login")]
        public async Task<IActionResult> Login([FromBody]SocialAuthenticationRequest request, CancellationToken cancellationToken)
        {
            return Ok(await _socialService.SignIn(request, cancellationToken));
        }
    }
}
