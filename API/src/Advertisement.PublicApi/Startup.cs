using Advertisement.Infrastructure;
using Advertisement.Infrastructure.DataAccess.RepositoryInDataBase;
using Advertisement.Infrastructure.TimedHosted.Implementations;
using Advertisement.PublicApi.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace Advertisement.PublicApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 5000;
            });

            services
                .AddControllers();

            services
                .AddApplicationModule()
                .AddHttpContextAccessor()
                .AddDataAccessModule(c =>
                    c.InDataBase(Configuration.GetConnectionString("PostgresDb")))
                .AddIdentity(Configuration);
            
            services.
                AddHostedService<TimedHostedService>();

            services.AddSwaggerModule();
            services.AddAutoMapperModule();
            services.AddSerilogModule();

            services.AddApplicationException(config => { config.DefaultErrorStatusCode = 500; });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Init migrations
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataBaseContext>();
            db.Database.Migrate();
                
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicApi v1"));
            
            app.UseHttpsRedirection();

            app.UseApplicationException();

            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}