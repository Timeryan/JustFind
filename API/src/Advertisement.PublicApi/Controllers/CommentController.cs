using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Comment.Contracts.CreateComment;
using Advertisement.Application.Services.Comment.Contracts.DeleteComment;
using Advertisement.Application.Services.Comment.Contracts.GetPagedComment;
using Advertisement.Application.Services.Comment.Contracts.UpdateComment;
using Advertisement.Application.Services.Comment.Interfaces;
using Advertisement.PublicApi.Models.Comment;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Advertisement.PublicApi.Controllers
{

    [Route("comments")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _service;
        private readonly IMapper _mapper;

        public CommentController(ICommentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateComment(CommentCreateDto request, CancellationToken cancellationToken)
        {
            var response = await _service.CreateComment(_mapper.Map<CreateCommentRequest>(request), cancellationToken);
            return Created($"/comment/{response.Id}", new { });
        }
        
        [HttpGet("getPaged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedComment([FromQuery] CommentGetPagedDto request ,CancellationToken cancellationToken)
        {
            return Ok(await _service.GetPagedComment(_mapper.Map<GetPagedCommentRequest>(request), cancellationToken));
        }
        
        [HttpPut("update")]
        public async Task<IActionResult> UpdateComment(CommentUpdateDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateComment(_mapper.Map<UpdateCommentRequest>(request), cancellationToken);
            return Ok();
        }
        
        [HttpDelete("delete")] 
        public async Task<IActionResult> DeleteComment([FromQuery] CommentDeleteDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteComment(_mapper.Map<DeleteCommentRequest>(request), cancellationToken);
            return NoContent();
        }
    }
}
