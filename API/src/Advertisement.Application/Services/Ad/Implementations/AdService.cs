using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Repository;
using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Ad.Contracts.DeleteAd;
using Advertisement.Application.Services.Ad.Contracts.Exceptions;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using Advertisement.Application.Services.Ad.Contracts.ModerateAd;
using Advertisement.Application.Services.Ad.Contracts.UpdateAd;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Category.Contracts.GetByIdCategory;
using Advertisement.Application.Services.Category.Interfaces;
using Advertisement.Application.Services.Identity.Interfaces;
using Advertisement.Application.Services.Photo.Contracts.SetAdOwnerPhoto;
using Advertisement.Application.Services.Photo.Interface;
using Advertisement.Domain.Shared;
using AutoMapper;

namespace Advertisement.Application.Services.Ad.Implementations
{
    public sealed class AdService : IAdService
    {
        private readonly IAdRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        private readonly IPhotoService _photoService;

        public AdService(IAdRepository repository, IIdentityService identityService, IMapper mapper, ICategoryService categoryService, IPhotoService photoService, IUserRepository userRepository)
        {
            _repository = repository;
            _identityService = identityService;
            _mapper = mapper;
            _categoryService = categoryService;
            _photoService = photoService;
            _userRepository = userRepository;
        }
        public async Task<CreateAdResponse> CreateAd(CreateAdRequest request, CancellationToken cancellationToken)
        {
            var identityUser = await _identityService.GetCurrentIdentityUserId(cancellationToken);
            if (identityUser == null)
                throw new NoUserForAdCreationException(
                    $"Попытка создания объявления [{request.Name}] без пользователя.");

            var domainUser = await _userRepository.FindById(new Guid(identityUser.Id), cancellationToken);
            if (!domainUser.Active)
                throw new UserNoHaveRightToCreateAdException("Данный пользователь заблокирован, свяжитесь с администрацией для уточнения деталей.");

            var ad = _mapper.Map<Domain.Ad>(request);
            ad.OwnerId = new Guid(identityUser.Id);
            ad.Status = Domain.Shared.Statuses.OnSale;
            await _repository.Save(ad, cancellationToken);
            
            foreach (var photoId in request.Photos)
            {
                await _photoService.SetAdOwnerPhoto(new SetAdOwnerPhotoRequest()
                {
                    Id = photoId,
                    AdOwnerId = ad.Id,

                },cancellationToken);
            }
            return _mapper.Map<CreateAdResponse>(ad);
        }

        public async Task<GetByIdAdResponse> GetById(GetByIdAdRequest request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);
            if (ad == null)
                throw new NoAdFoundException(request.Id);

            var adResponse = _mapper.Map<GetByIdAdResponse>(ad);

            var userId = await _identityService.GetCurrentIdentityUserId(cancellationToken);

            if (userId.Id != null)
            {
                adResponse.OwnerNumber = ad.Owner.Phone;
            }
                
            ICollection<GetByIdAdPhotoItem> photos = new List<GetByIdAdPhotoItem>();
            foreach (var photo in ad.Photos)
            {
                photos.Add(new GetByIdAdPhotoItem()
                {
                    Id = photo.Id,
                    KodBase64 = photo.KodBase64
                });
            }
            adResponse.Photos = photos;
            return adResponse;
        }

        public async Task UpdateAd(UpdateAdRequest request, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(request.Id, cancellationToken);
            if (ad == null)
                throw new NoAdFoundException(request.Id);
            
            var user = await _identityService.GetCurrentIdentityUserId(cancellationToken);
            if (user.Id != ad.OwnerId.ToString())
                throw new UserNoHaveRightsToEditAdException(
                    $"Попытка редактирония объявления [{request.Name}] пользователем без прав.");
            var domainUser = await _userRepository.FindById(new Guid(user.Id), cancellationToken);
            if (!domainUser.Active)
                throw new UserNoHaveRightToCreateAdException("Данный пользователь заблокирован, свяжитесь с администрацией для уточнения деталей.");
            
            foreach (var photoId in request.Photos)
            {
                await _photoService.SetAdOwnerPhoto(new SetAdOwnerPhotoRequest()
                {
                    Id = photoId,
                    AdOwnerId = ad.Id,
                },cancellationToken);
            }
            await _repository.Save(_mapper.Map(request, ad), cancellationToken);
        }

        public async Task<GetPagedAdResponse> GetPagedAd(GetPagedAdRequest request, CancellationToken cancellationToken)
        {
            var total = await _repository.Count(cancellationToken);
            if (total == 0)
            {
                return _mapper.Map<GetPagedAdResponse>(request);
            }

            var authUserId = await _identityService.GetCurrentIdentityUserId(cancellationToken);

            var categoriesId = new List<Guid>();
            if (request.SearchCategoryId != Guid.Empty)
            {
                var categoryResponse =
                    await _categoryService.GetByIdCategory(new GetByIdCategoryRequest {Id = request.SearchCategoryId},
                        cancellationToken);
                var categoryTree = new Queue<Domain.Category>();
                categoryTree.Enqueue(new Domain.Category()
                { 
                    Id = request.SearchCategoryId,
                    Name = categoryResponse.Name, 
                    ParentCategoryId = categoryResponse.ParentCategoryId,
                    ChildCategories = categoryResponse.ChildCategories
                });
                while (categoryTree.Count > 0)
                {
                    var current = categoryTree.Dequeue();
                    categoriesId.Add(current.Id);
                    foreach (var currentSubCategory in current.ChildCategories)
                        categoryTree.Enqueue(currentSubCategory);
                }
            }

            GetLocationKladrIdStartAndEnd(request.SearchLocationKladrId, out var searchLocationKladrIdStart, out var searchLocationKladrIdEnd);

            var ads = await _repository.FindWhere(
                find => 
                    find.Name.Contains(request.SearchName) && 
                    ((find.CategoryId != null && categoriesId.Contains((Guid) find.CategoryId)) || request.SearchCategoryId == Guid.Empty) &&
                    ((find.LocationKladrId >= searchLocationKladrIdStart && find.LocationKladrId < searchLocationKladrIdEnd) || request.SearchLocationKladrId == null) && 
                    (find.OwnerId == request.SearchOwnerId || request.SearchOwnerId == Guid.Empty) &&
                    (request.SearchOwnerId == Guid.Empty && find.Status == Statuses.OnSale || 
                     request.SearchOwnerId == find.OwnerId && request.SearchOwnerId.ToString() == authUserId.Id ||
                     request.SearchOwnerId == find.OwnerId && request.SearchOwnerId.ToString() != authUserId.Id && find.Status == Statuses.OnSale )&&
                    (find.OwnerId == request.SearchOwnerId || find.Status != Statuses.Rejected)
                , cancellationToken);
            
            return new GetPagedAdResponse
            {
                Items = SortAd(request.SortParam, request.SortDirection, ads)
                    .Skip(request.Offset)
                    .Take(request.Limit)
                    .Select(a => _mapper.Map<GetPagedAdResponseItem>(a)),
                Total = ads.Count(),
                Offset = request.Offset,
                Limit = request.Limit
            };
        }
        
        public async Task DeleteAd(DeleteAdRequest adRequest, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(adRequest.Id, cancellationToken);
            if (ad == null)
                throw new NoAdFoundException(adRequest.Id);
            ad.Status = Statuses.Closed;

            await _repository.Save(ad, cancellationToken);
        }

        private IEnumerable<Domain.Ad> SortAd(string sortParam,bool sortDirection, IEnumerable<Domain.Ad> ads)
        {
            var sortAds = ads;
            
            if (sortParam == "Price")
                sortAds = sortDirection ? ads.OrderBy(s => s.Price) : ads.OrderByDescending(s => s.Price);
            
            if (sortParam == "Date")
                sortAds = sortDirection ? ads.OrderBy(s => s.UpdatedAt) : ads.OrderByDescending(s => s.UpdatedAt);
            
            return sortAds;
        }
        
        private static void GetLocationKladrIdStartAndEnd(long? locationKladrId, out long? locationKladrIdStart, out long? locationKladrIdEnd)
        {
            if (locationKladrId == null)
            {
                locationKladrIdStart = null;
                locationKladrIdEnd = null;
                return;   
            }

            var locationKladrIdstr = locationKladrId.ToString().Substring(0,11);
            locationKladrIdStart = Convert.ToInt64(locationKladrIdstr);

            if (locationKladrIdstr.Substring(8) != "000")//проверка на код населенного пункта
            {
                locationKladrIdEnd = Convert.ToInt64(locationKladrIdstr) + 1;
                return;
            }
            if (locationKladrIdstr.Substring(5,3) != "000")//проверка на код города
            {
                locationKladrIdEnd = Convert.ToInt64(locationKladrIdstr) + 1000;
                return;
            }
            if (locationKladrIdstr.Substring(2,3) != "000")//проверка на код района
            {
                locationKladrIdEnd = Convert.ToInt64(locationKladrIdstr) + 1000000;
                return;
            }
            if (locationKladrIdstr.Substring(0,2) != "00")//проверка на код субъекта
            {
                locationKladrIdEnd = Convert.ToInt64(locationKladrIdstr) + 1000000000;
                return;
            }
            locationKladrIdEnd = Convert.ToInt64(locationKladrIdstr);
        }

        public async Task Moderate(ModerateAdRequest moderateAdRequest, CancellationToken cancellationToken)
        {
            var ad = await _repository.FindById(moderateAdRequest.AdId, cancellationToken);
            if (ad == null)
                throw new NoAdFoundException(moderateAdRequest.AdId);
            if (moderateAdRequest.Approved)
                ad.Status = Domain.Shared.Statuses.OnSale;
            else
            {
                ad.Status = Domain.Shared.Statuses.Rejected;
                ad.ModerationComment = moderateAdRequest.Comment;
            }
            await _repository.Save(ad, cancellationToken);
        }

        public async Task RejectAllAdsOfUser(Guid userId, string moderationComment, CancellationToken cancellation)
        {
            if (!(await _identityService.CurrentUserAdmin(cancellation)))
                throw new UserNoHaveRightsToEditAdException("Отклонение всех объявлений пользователя доступно только для администратора.");

            var ads = await _repository.FindWhere(x => x.OwnerId == userId, cancellation);
            foreach(var ad in ads)
            {
                ad.Status = Statuses.Rejected;
                ad.ModerationComment = moderationComment;
                await _repository.Save(ad, cancellation);
            }
        }

        public async Task DeleteAdAfterMonth(CancellationToken cancellationToken)
        {
            var ads = await _repository.FindWhere(ad =>
                    (DateTime.UtcNow - ad.UpdatedAt > new TimeSpan(30, 0, 0, 0) && ad.Status == Statuses.OnSale)
            , cancellationToken);

            foreach (var ad in ads)
            {
                ad.Status = Statuses.Closed;
                await _repository.Save(ad, cancellationToken);
            }
        }

        public async Task UpdateAdStatus(Guid adId, Statuses status, string moderationComment, CancellationToken cancellation)
        {
            if (!(await _identityService.CurrentUserAdmin(cancellation)))
                throw new UserNoHaveRightsToEditAdException("Отклонение всех объявлений пользователя доступно только для администратора.");

            var ad = await _repository.FindById(adId, cancellation);
            ad.ModerationComment = moderationComment;
            ad.Status = status;

            await _repository.Save(ad, cancellation);
        }
    }
}