using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Category.Contracts.CreateCategory;
using Advertisement.Application.Services.Category.Contracts.DeleteCategory;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Contracts.UpdateCategory;
using Advertisement.Application.Services.Category.Interfaces;
using Advertisement.Domain;
using Advertisement.PublicApi.Models.Category;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, ICategoryService service)
        {
            _mapper = mapper;
            _service = service;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryCreateDto request, CancellationToken cancellationToken)
        {
            var response = await _service.CreateCategory(_mapper.Map<CreateCategoryRequest>(request), cancellationToken);
            return Created($"/category/{response.Id}", new { });
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> Update(CategoryUpdateDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateCategory(_mapper.Map<UpdateCategoryRequst>(request), cancellationToken);
            return Ok();
        }
        
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _service.GetAllCategory(cancellationToken));
        }
        
        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] CategoryGetByIdDto request, CancellationToken cancellationToken)
        { 
            return Ok(await _service.GetByIdCategory(_mapper.Map<GetByIdCategoryRequest>(request), cancellationToken));
        }
        
        [HttpDelete("delete")] 
        public async Task<IActionResult> DeleteComment([FromQuery] CategoryDeleteDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteCategory(_mapper.Map<DeleteCategoryRequest>(request), cancellationToken);
            return NoContent();
        }
    }
}