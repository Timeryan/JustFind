using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Ad.Contracts;
using Advertisement.Application.Services.Ad.Contracts.CreateAd;
using Advertisement.Application.Services.Ad.Contracts.DeleteAd;
using Advertisement.Application.Services.Ad.Contracts.GetById;
using Advertisement.Application.Services.Ad.Contracts.GetPagedAd;
using Advertisement.Application.Services.Ad.Contracts.ModerateAd;
using Advertisement.Application.Services.Ad.Contracts.UpdateAd;
using Advertisement.Domain.Shared;

namespace Advertisement.Application.Services.Ad.Interfaces
{
    public interface IAdService
    {
        Task<CreateAdResponse> CreateAd(CreateAdRequest request, CancellationToken cancellationToken);

        Task<GetByIdAdResponse> GetById(GetByIdAdRequest request, CancellationToken cancellationToken);

        Task DeleteAd(DeleteAdRequest adRequest, CancellationToken cancellationToken);

        Task UpdateAd(UpdateAdRequest adRequest, CancellationToken cancellationToken);
        
        Task<GetPagedAdResponse> GetPagedAd(GetPagedAdRequest request, CancellationToken cancellationToken);
        
        Task Moderate(ModerateAdRequest moderateAdRequest, CancellationToken cancellationToken);
        Task RejectAllAdsOfUser(Guid userId, string moderationComment, CancellationToken cancellation);
        Task UpdateAdStatus(Guid adId, Statuses status, string moderationComment, CancellationToken cancellation);
        Task DeleteAdAfterMonth(CancellationToken cancellationToken = default);
    }
}