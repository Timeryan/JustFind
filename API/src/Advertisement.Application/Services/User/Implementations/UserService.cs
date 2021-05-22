using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Common;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Identity.Contracts.CreateIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.DeleteIdentityUser;
using Advertisement.Application.Services.Identity.Contracts.Update;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.User.Contracts.DeleteDomainUser;
using Advertisement.Application.Services.User.Contracts.GetAllDomainUser;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Contracts.RegisterDomainUser;
using Advertisement.Application.Services.User.Contracts.UpdateDomainUser;
using Advertisement.Application.Services.User.Exceptions;
using Advertisement.Application.Services.User.Interfaces;
using AutoMapper;

namespace Advertisement.Application.Services.User.Implementations
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IIdentityService identityService, IMapper mapper)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task DeleteDomainUser(DeleteDomainUserRequest domainUserRequest, CancellationToken cancellationToken)
        {
            var user = await _repository.FindById(domainUserRequest.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundUserException(domainUserRequest.Id);
            }
            var response = await _identityService.DeleteIdentityUser(_mapper.Map<DeleteIdentityUserRequest>(domainUserRequest),cancellationToken);
            if (!response.IsSuccess)
            {
                throw new UserDeleteException(string.Join(';', response.Errors));
            }
            await _repository.DeleteById(domainUserRequest.Id, cancellationToken);
        }

        public async Task<GetAllDomainUserResponse> GetAllDomainUser(CancellationToken cancellationToken)
        {
            var users = await _repository.Get(cancellationToken);
            return new GetAllDomainUserResponse
            {
                Items = users.Select(item => _mapper.Map<GetAllDomainUserResponseItem>(item)),
            };
        }
        public async Task<RegisterDomainUserResponse> RegisterDomainUser(RegisterDomainUserRequest request, CancellationToken cancellationToken)
        {
            var url = "https://www.google.com/recaptcha/api/siteverify";
            var privateKey = "6Le5xa4aAAAAAOhIYPFP6ghfUxbf0bajoGC_ZniE";

            HttpClient httpClient = new HttpClient();

            var strUrl = $"{url}?secret={privateKey}&response={request.ReCaptchaResponse}";
            var responseFromReCaptcha = await httpClient.PostAsync(strUrl,new StringContent(""), cancellationToken);
            var str = await responseFromReCaptcha.Content.ReadAsStringAsync(cancellationToken);
            var dataReCaptcha =  JsonSerializer.Deserialize<DataReCaptcha>(str);

            if (dataReCaptcha != null && dataReCaptcha.Success)
            {
                var response = await _identityService.CreateIdentityUser(
                    _mapper.Map<CreateIdentityUserRequest>(request), cancellationToken);

                if (!response.IsSuccess) throw new UserRegisteredException(string.Join(';', response.Errors));
            
                var domainUser = _mapper.Map<Domain.User>(request);
                domainUser.Id = new Guid(response.UserId);
            
                await _repository.Save(domainUser, cancellationToken);
            
                return _mapper.Map<RegisterDomainUserResponse>(response);
            }

            throw new UserRegisteredReCaptchaException("Капча на пройдена");
        }

        public async Task<GetByIdDomainUserResponse> GetByIdDomainUser(GetByIdDomainUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindById(request.Id, cancellationToken);
            
            if (user == null)
                throw new NotFoundUserException(request.Id);

            var result = _mapper.Map<GetByIdDomainUserResponse>(user);
            result.Role = await _identityService.GetIdentityUserRole(request.Id.ToString());

            return result;
        }

        public async Task UpdateDomainUser(UpdateDomainUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindById(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundUserException(request.Id);

            var response = await _identityService.UpdateIdentityUser(_mapper.Map<UpdateIdentityUserRequest>(request), cancellationToken);
            if (!response.IsSuccess)
                throw new UserUpdateException(string.Join(';', response.Errors));
            
            await _repository.Save(_mapper.Map(request, user), cancellationToken);
        }

        public async Task UpdatePhotoDomainUser(UpdatePhotoDomainUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _repository.FindById(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundUserException(request.Id);
            if (request.Photo != null) 
                user.Photo = Convert.ToBase64String(request.Photo, 0, request.Photo.Length);
            await _repository.Save(user, cancellationToken);
        }
    }
}