using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Application.Services.User.Contracts.DeleteDomainUser;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Contracts.RegisterDomainUser;
using Advertisement.Application.Services.User.Contracts.UpdateDomainUser;
using Advertisement.Application.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Advertisement.PublicApi.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Advertisement.PublicApi.Controllers
{
    [Route("users")]
    [ApiController]
    [AllowAnonymous]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        
        public UserController(IUserService userService, IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto request, CancellationToken cancellationToken)
        {
            var response = await _userService.RegisterDomainUser(_mapper.Map<RegisterDomainUserRequest>(request), cancellationToken);
            return Created($"users/{response.UserId}", new { });
        }
        
        [HttpGet("confirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        { 
            await _identityService.ConfirmEmail(userId, code);
            return Redirect("https://justfind.ru.com");
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto request, CancellationToken cancellationToken)
        {
            return Ok(await _identityService.CreateIdentityUserToken( _mapper.Map<CreateTokenRequest>(request), cancellationToken));
        }
        
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _userService.GetAllDomainUser(cancellationToken));
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] UserGetByIdDto request, CancellationToken cancellationToken)
        { 
            return Ok(await _userService.GetByIdDomainUser(_mapper.Map<GetByIdDomainUserRequest>(request), cancellationToken));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserUpdateDto request, CancellationToken cancellationToken)
        {
            await _userService.UpdateDomainUser(_mapper.Map<UpdateDomainUserRequest>(request), cancellationToken);
            return Ok();
        }
        
        [HttpPut("updatePhoto")]
        public async Task<IActionResult> UpdatePhoto(IFormFile photo, Guid id, CancellationToken cancellationToken)
        {
            UpdatePhotoDomainUserRequest request = new UpdatePhotoDomainUserRequest()
            {
                Id = id
            };
            await using (var ms = new MemoryStream())
            await using (var fs = photo.OpenReadStream())
            {
                await fs.CopyToAsync(ms, cancellationToken);
                request.Photo = ms.ToArray();
            }
            
            await _userService.UpdatePhotoDomainUser(request, cancellationToken);
            return Ok();
        }
        

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromQuery] UserDeleteDto request, CancellationToken cancellationToken)
        {
            await _userService.DeleteDomainUser(_mapper.Map<DeleteDomainUserRequest>(request), cancellationToken);
            return NoContent();
        }
    }
}