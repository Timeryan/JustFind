using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Petition.Contracts.Create;
using Advertisement.Application.Services.Petition.Interface;
using Advertisement.PublicApi.Models.Petition;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{
    [Route("petition")]
    [ApiController]
    public class PetitionController : ControllerBase
    {
        private readonly IPetitionService _petitionService;
        private readonly IMapper _mapper;

        public PetitionController(IPetitionService petitionService, IMapper mapper)
        {
            _petitionService = petitionService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePetitionDto createPetitionDto, CancellationToken cancellationToken)
        {
            await _petitionService.CreatePetition(_mapper.Map<CreatePetitionRequest>(createPetitionDto), cancellationToken);
            return NoContent();
        }
    }
}