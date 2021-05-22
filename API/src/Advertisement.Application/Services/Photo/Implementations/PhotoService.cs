using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Ad.Contracts.DeleteAd;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Photo.Contracts.CreatePhoto;
using Advertisement.Application.Services.Photo.Contracts.DeletePhoto;
using Advertisement.Application.Services.Photo.Contracts.SetAdOwnerPhoto;
using Advertisement.Application.Services.Photo.Exceptions;
using Advertisement.Application.Services.Photo.Interface;
using AutoMapper;

namespace Advertisement.Application.Services.Photo.Implementations
{
    public class PhotoService : IPhotoService
    {
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _repository;

        public PhotoService(IMapper mapper, IPhotoRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CreatePhotoResponse> CreatePhoto(CreatePhotoRequest request, CancellationToken cancellationToken)
        {
            if (request.Photo.Length > 5242880)
            {
                throw new OverflowMaxSizePhotoException();
            }

            var photo = new Domain.Photo()
            {
                KodBase64 = Convert.ToBase64String(request.Photo, 0, request.Photo.Length)
            };
            await _repository.Save(photo, cancellationToken);
            return _mapper.Map<CreatePhotoResponse>(photo);
        }

        
        public async Task DeletePhoto(DeletePhotoRequest request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);
            if (ad == null)
                throw new NoPhotoFoundException(request.Id);
            
            await _repository.DeleteById(request.Id,cancellationToken);
        }

        public async Task SetAdOwnerPhoto(SetAdOwnerPhotoRequest request, CancellationToken cancellationToken)
        {
            var photo = await _repository.FindById(request.Id, cancellationToken);
            photo.AdOwnerId = request.AdOwnerId;
            await _repository.Save(photo,cancellationToken);
        }

        public async Task DeleteAllPhotoWithOutAdOwner(CancellationToken cancellationToken = default)
        {
            var photos = await _repository.FindWhere(el => el.AdOwnerId == null, cancellationToken);
            if(photos != null)
                foreach (var photo in photos)
                {
                    await _repository.DeleteById(photo.Id, cancellationToken);
                }
        }
    }
}