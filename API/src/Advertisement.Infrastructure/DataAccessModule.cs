using System;
using Advertisement.Application.Repository;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess;
using Advertisement.Infrastructure.DataAccess.RepositoryInDataBase;
using Advertisement.Infrastructure.DataAccess.RepositoryInDataBase.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Advertisement.Infrastructure
{
    public static class DataAccessModule
    {
        public sealed class Configurator
        {
            internal IServiceCollection Services { get; init; }
        }

        public static IServiceCollection AddDataAccessModule(
            this IServiceCollection services,
            Action<Configurator> configure)
        {
            var configurator = new Configurator
            {
                Services = services
            };
            configure(configurator);

            return services;
        }

        public static Configurator InMemory(this Configurator configurator)
        {
            configurator.Services
                .AddTransient<IAdRepository, AdBaseRepositoryInMemory>()
                .AddTransient<IUserRepository, UserBaseRepositoryInMemory>()
                .AddTransient<ICommentRepository, CommentRepositoryInMemory>();

            return configurator;
        }

        public static void InDataBase(this Configurator configurator, string connectionString)
        {
            configurator.Services.AddDbContextPool<DataBaseContext>(options =>
            {
                options.UseNpgsql(connectionString, builder =>
                    builder.MigrationsAssembly(
                        typeof(DataAccessModule).Assembly.FullName)).UseLazyLoadingProxies();
            });

            configurator.Services.AddScoped<IAdRepository, AdRepositoryInDataBase>();
            configurator.Services.AddScoped<IUserRepository, UserRepositoryInDataBase>();
            configurator.Services.AddScoped<ICommentRepository, CommentRepositoryInDataBase>();
            configurator.Services.AddScoped<ICategoryRepository, CategoryRepositoryInDataBase>();
            configurator.Services.AddScoped<IPhotoRepository, PhotoRepositoryInDataBase>();
            configurator.Services.AddScoped<IPetitionRepository, PetitionRepositoryInDataBase>();

        }
    }
}