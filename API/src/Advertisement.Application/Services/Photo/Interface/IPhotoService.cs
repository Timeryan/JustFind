using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Application.Services.Photo.Contracts.DeletePhoto;
using Advertisement.Application.Services.Photo.Contracts.SetAdOwnerPhoto;

namespace Advertisement.Application.Services.Photo.Interface
{
    public interface IPhotoService
    {
        Task<CreatePhotoResponse> CreatePhoto(CreatePhotoRequest commentRequest, CancellationToken cancellationToken);
        Task DeletePhoto(DeletePhotoRequest commentRequest, CancellationToken cancellationToken);
        Task SetAdOwnerPhoto(SetAdOwnerPhotoRequest request, CancellationToken cancellationToken);
        Task DeleteAllPhotoWithOutAdOwner(CancellationToken cancellationToken = default);

    }
}