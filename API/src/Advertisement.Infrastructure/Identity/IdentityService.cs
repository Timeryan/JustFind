using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Advertisement.Application.Common;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUserToken;
using Advertisement.Application.Services.Identity.Contracts.DeleteIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.GetCurrentIdentityUserId;
using Advertisement.Application.Services.Identity.Contracts.IsInIdentityUserRole;
using Advertisement.Application.Services.Identity.Contracts.Update;
using Advertisement.Application.Services.Identity.Exceptions;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.Social.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace Advertisement.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager,
            IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<GetCurrentIdentityUserIdResponse> GetCurrentIdentityUserId(
            CancellationToken cancellationToken = default)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            return new GetCurrentIdentityUserIdResponse()
            {
                Id = _userManager.GetUserId(claimsPrincipal)
            };
        }

        public async Task<bool> CurrentUserAdmin(CancellationToken cancellationToken)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext?.User;
            var user = await _userManager.GetUserAsync(claimsPrincipal);

            return await _userManager.IsInRoleAsync(user, RoleConstants.AdminRole);
        }

        public async Task<IsInIdentityUserRoleResponse> IsInIdentityUserRole(IsInIdentityUserRoleRequest request,
            CancellationToken cancellationToken = default)
        {
            var identityUser = await _userManager.FindByIdAsync(request.Id);
            if (identityUser == null)
            {
                throw new IdentityUserNotFoundException("Пользователь не найден");
            }

            return new IsInIdentityUserRoleResponse()
            {
                IsInRole = await _userManager.IsInRoleAsync(identityUser, request.Role)
            };
        }

        public async Task SendConfirmMail(string userId, CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await _userManager.FindByIdAsync(userId); 
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                var callbackUrl = $"https://justfind.ru.com:5000/users/confirmEmail?userId={identityUser.Id}&code={HttpUtility.UrlEncode(code)}";
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(identityUser.Email, "Confirm your account",
                    $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");
            }
            catch (Exception e)
            {
                throw new IdentityEmailSendException("Проблемы с отправкой письма на почту, проверьте Email ");
            }
        }

        public async Task<CreateIdentityUserResponse> CreateIdentityUser(CreateIdentityUserRequest request,
            CancellationToken cancellationToken = default)
        {
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new IdentityUserEmailException("Пользователь с такой почтой уже существует");
            }

            if (await _userManager.FindByNameAsync(request.Login) != null)
            {
                throw new IdentityUserLoginException("Пользователь с таким логином уже существует");
            }

            var identityUser = new IdentityUser
            {
                UserName = request.Login,
                Email = request.Email,
                PhoneNumber = request.Phone
            };
            
            
            var identityResult = await _userManager.CreateAsync(identityUser, request.Password);
            

            if (identityResult.Succeeded)
            {
                
                await _userManager.AddToRoleAsync(identityUser, request.Role);
            }
            await SendConfirmMail(identityUser.Id, cancellationToken);
            
            if (identityResult.Succeeded)
            {
                return new CreateIdentityUserResponse
                {
                    UserId = identityUser.Id,
                    IsSuccess = true
                };
            }

            return new CreateIdentityUserResponse
            {
                IsSuccess = false,
                Errors = identityResult.Errors.Select(x => x.Description).ToArray()
            };
        }

        public async Task ConfirmEmail(string userId, string code, CancellationToken cancellationToken)
        {
            if (userId == null || code == null)
            {
                throw new IdentityUserNotFoundException("Не найден пользователь");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new IdentityUserNotFoundException("Не найден пользователь");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return;
            throw new IdentityException("Ошибка подтверждения пользователя");
        }

        public async Task<CreateTokenResponse> CreateIdentityUserToken(CreateTokenRequest request,
            CancellationToken cancellationToken = default)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);
            if (identityUser == null)
                throw new IdentityUserNotFoundException("Неверная почта или пароль");

            var passwordCheckResult = await _userManager.CheckPasswordAsync(identityUser, request.Password);
            if (!passwordCheckResult)
            {
                throw new IdentityUserNotFoundException("Неверная почта или пароль");
            }

            var emailConfirmCheck = await _userManager.IsEmailConfirmedAsync(identityUser);
            if (!emailConfirmCheck)
            {
                throw new IdentityUserNotFoundException("Почта не подтверждена");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new CreateTokenResponse
            {
                Id = identityUser.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        public async Task<string> GetIdentityUserRole(string id)
        {
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
                throw new IdentityUserNotFoundException("Пользователь не найден");
            
            var roles = await _userManager.GetRolesAsync(identityUser);

            return roles.FirstOrDefault();
        }

        public async Task<UpdateIdentityUserResponse> UpdateIdentityUser(UpdateIdentityUserRequest request,
            CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByIdAsync(request.Id);
            if (identityUser == null)
                throw new IdentityUserNotFoundException("Пользователь не найден");

            identityUser.UserName = request.Login ?? identityUser.UserName;
            identityUser.Email = request.Email ?? identityUser.Email;
            identityUser.PhoneNumber = request.Phone ?? identityUser.PhoneNumber;

            var updateResult = await _userManager.UpdateAsync(identityUser);

            if (!updateResult.Succeeded)
                return new UpdateIdentityUserResponse()
                {
                    IsSuccess = false,
                    Errors = updateResult.Errors.Select(x => x.Description).ToArray()
                };

            if (request.NewPassword != null)
            {
                var passwordChangeResult =
                    await _userManager.ChangePasswordAsync(identityUser, request.CurrentPassword, request.NewPassword);
                if (!passwordChangeResult.Succeeded)
                    return new UpdateIdentityUserResponse()
                    {
                        IsSuccess = false,
                        Errors = passwordChangeResult.Errors.Select(x => x.Description).ToArray()
                    };
            }

            if (request.Role != null)
            {
                var roleChangeResult = await _userManager.AddToRoleAsync(identityUser, request.Role);
                if (!roleChangeResult.Succeeded)
                    return new UpdateIdentityUserResponse()
                    {
                        IsSuccess = false,
                        Errors = roleChangeResult.Errors.Select(x => x.Description).ToArray()
                    };
            }

            return new UpdateIdentityUserResponse()
            {
                IsSuccess = true,
            };
        }

        public async Task<DeleteIdentityUserResponse> DeleteIdentityUser(DeleteIdentityUserRequest request,
            CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByIdAsync(request.Id);
            if (identityUser == null)
                throw new IdentityUserNotFoundException("Пользователь не найден");

            var identityResponse = await _userManager.DeleteAsync(identityUser);
            if (!identityResponse.Succeeded)
                return new DeleteIdentityUserResponse()
                {
                    IsSuccess = false,
                    Errors = identityResponse.Errors.Select(x => x.Description).ToArray()
                };
            return new DeleteIdentityUserResponse()
            {
                IsSuccess = true,
            };
        }

        public async Task<CreateTokenResponse> SocialSignIn(SocialAuthenticationRequest request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.FindByLoginAsync(request.Provider, request.Id);

            if (identityUser != null)
                return await CreateTokenByIdentityUser(identityUser);
            
            identityUser = await _userManager.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if (identityUser != null)
            {
                await _userManager.AddLoginAsync(identityUser, new UserLoginInfo(request.Provider, request.Id.ToString(), request.Provider));
                return await CreateTokenByIdentityUser(identityUser);
            }

            throw new IdentityUserNotFoundException($"Пользователя с Email = {request.Email} не существует, пожалуйста пройдите процедуру регистрации.");
        }

        private async Task<CreateTokenResponse> CreateTokenByIdentityUser(IdentityUser identityUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id)
            };

            var userRoles = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])), SecurityAlgorithms.HmacSha256)
            );

            return new CreateTokenResponse
            {
                Id = identityUser.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}