using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Common;
using Advertisement.Application.Services.Ad.Contracts.ModerateAd;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Petition.Contracts.GetPagedPetition;
using Advertisement.Application.Services.Petition.Contracts.Review;
using Advertisement.Application.Services.Petition.Interface;
using Advertisement.PublicApi.Models.Advertisement;
using Advertisement.PublicApi.Models.Petition;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{
    [Route("admin")]
    [Authorize(Roles = RoleConstants.AdminRole)]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly IPetitionService _petitionService;
        private readonly IMapper _mapper;

        public AdminController(IAdService adService, IMapper mapper, IPetitionService petitionService)
        {
            _petitionService = petitionService;
            _adService = adService;
            _mapper = mapper;
        }

        [HttpPut("moderate")]
        public async Task<IActionResult> Moderate(AdModerationDto adModerationResult, CancellationToken cancellationToken)
        {
            await _adService.Moderate(_mapper.Map<ModerateAdRequest>(adModerationResult), cancellationToken);
            return NoContent();
        }

        [HttpPost("review-petition")]
        public async Task<IActionResult> ReviewPetition(ReviewPetitionDto reviewPetition, CancellationToken cancellationToken)
        {
            await _petitionService.ReviewPetition(_mapper.Map<ReviewPetitionRequest>(reviewPetition), cancellationToken);
            return NoContent();
        }

        [HttpGet("get-paged")]
        public async Task<IActionResult> GetPaged([FromQuery]GetPagedPetitionDto getPagedPetitionDto, CancellationToken cancellationToken)
        {
            var data = await _petitionService.GetPaged(_mapper.Map<GetPagedPetitionRequest>(getPagedPetitionDto), cancellationToken);
            return Ok(data);
        }
    }
}