using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Application.Services.Photo.Contracts.DeletePhoto;
using Advertisement.Application.Services.Photo.Interface;
using Advertisement.PublicApi.Models.Advertisement;
using Advertisement.PublicApi.Models.Photo;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{
    [Route("photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;
        private readonly IMapper _mapper;

        public PhotoController(IPhotoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(IFormFile file, CancellationToken cancellationToken)
        {
            CreatePhotoResponse response;
            await using (var ms = new MemoryStream())
            await using (var fs = file.OpenReadStream())
            {
                await fs.CopyToAsync(ms, cancellationToken);
                
                response = await _service.CreatePhoto(new CreatePhotoRequest()
                {
                    Photo = ms.ToArray()
                }, cancellationToken);
            }
            
            return Ok(response.Id); 
        }
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] PhotoDeleteDto request, CancellationToken cancellationToken)
        {
            await _service.DeletePhoto(_mapper.Map<DeletePhotoRequest>(request), cancellationToken);
            return NoContent();
        }
        
        [HttpDelete("deleteAll")]
        public async Task<IActionResult> Delete(CancellationToken cancellationToken)
        {
            await _service.DeleteAllPhotoWithOutAdOwner(cancellationToken);
            return NoContent();
        }
    }
}