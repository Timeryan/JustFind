using Advertisement.Application.Services.Ad.Implementations;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Category.Implementations;
using Advertisement.Application.Services.Category.Interfaces;
using Advertisement.Application.Services.User.Implementations;
using Advertisement.Application.Services.User.Interfaces;
using Advertisement.Application.Services.Comment.Interfaces;
using Advertisement.Application.Services.Comment.Implementations;
using Advertisement.Application.Services.Photo.Implementations;
using Advertisement.Application.Services.Photo.Interface;
using Microsoft.Extensions.DependencyInjection;
using Advertisement.Application.Services.Petition.Interface;
using Advertisement.Application.Services.Petition.Implementations;
using Advertisement.Application.Services.Social.Interfaces;

namespace Advertisement.Infrastructure
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdService, AdService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPetitionService, PetitionService>();
            services.AddScoped<ISocialService, SocialService>();

            return services;
        }
    }
}