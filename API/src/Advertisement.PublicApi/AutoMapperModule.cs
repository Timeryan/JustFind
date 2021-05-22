using Advertisement.Application.MapProfiles;
using Advertisement.PublicApi.MapProfiles;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.PublicApi
{
    public static class AutoMapperModule
    {
        public static IServiceCollection AddAutoMapperModule(this IServiceCollection services)
        {
            services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
            return services;
        }

        private static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(config =>
                {
                    config.AddProfile(new UserMapServiceProfile());
                    config.AddProfile(new AdMapServiceProfile());
                    config.AddProfile(new CommentMapServiceProfile());
                    config.AddProfile(new CategoryMapServiceProfile());
                    config.AddProfile(new PhotoMapServiceProfile());
                    config.AddProfile(new PetitionMapServiceProfile());
                    
                    config.AddProfile(new AdMapControllerProfile());
                    config.AddProfile(new AdPetitionControllerProfile());
                    config.AddProfile(new CommentMapControllerProfile());
                    config.AddProfile(new UserMapControllerProfile());
                    config.AddProfile(new CategoryMapControllerProfile());
                    config.AddProfile(new PhotoMapControllerProfile());
                }
            );
            configuration.AssertConfigurationIsValid();
                
            return configuration;
            
        }
    }
}