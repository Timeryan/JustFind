using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.User.Contracts.DeleteDomainUser;
using Advertisement.Application.Services.User.Contracts.GetAllDomainUser;
using Advertisement.Application.Services.User.Contracts.GetByIdDomainUser;
using Advertisement.Application.Services.User.Contracts.RegisterDomainUser;
using Advertisement.Application.Services.User.Contracts.UpdateDomainUser;

namespace Advertisement.Application.Services.User.Interfaces
{
    public interface IUserService
    {
        Task<RegisterDomainUserResponse> RegisterDomainUser(RegisterDomainUserRequest request, CancellationToken cancellationToken);
        Task<GetAllDomainUserResponse> GetAllDomainUser(CancellationToken cancellationToken);
        Task DeleteDomainUser(DeleteDomainUserRequest domainUserRequest, CancellationToken cancellationToken);
        Task<GetByIdDomainUserResponse> GetByIdDomainUser(GetByIdDomainUserRequest request, CancellationToken cancellationToken);
        Task UpdateDomainUser(UpdateDomainUserRequest request, CancellationToken cancellationToken);
        Task UpdatePhotoDomainUser(UpdatePhotoDomainUserRequest request, CancellationToken cancellationToken);
    }
}