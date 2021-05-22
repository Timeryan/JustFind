using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Common;
using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Ad.Contracts.DeleteAd;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using Advertisement.Application.Services.Ad.Contracts.ModerateAd;
using Advertisement.Application.Services.Ad.Contracts.UpdateAd;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.PublicApi.Models.Advertisement;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{
    [Route("advertisements")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdService _service;
        private readonly IMapper _mapper;
        public AdvertisementController(IAdService adService, IMapper mapper)
        {
            _service = adService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AdCreateDto request, CancellationToken cancellationToken)
        {
            var response = await _service.CreateAd(_mapper.Map<CreateAdRequest>(request), cancellationToken);
            return Ok(response.Id);
        }

        [HttpGet("getPaged")]
        public async Task<IActionResult> GetPaged([FromQuery] AdGetPagedDto request, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetPagedAd(_mapper.Map<GetPagedAdRequest>(request), cancellationToken));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery]AdGetByIdDto request, CancellationToken cancellationToken)
        {
            return Ok(await _service.GetById(_mapper.Map<GetByIdAdRequest>(request), cancellationToken));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(AdUpdateDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateAd(_mapper.Map<UpdateAdRequest>(request), cancellationToken);
            return Ok();
        }
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery]AdDeleteDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteAd(_mapper.Map<DeleteAdRequest>(request), cancellationToken);
            return NoContent();
        }

        
    }
}