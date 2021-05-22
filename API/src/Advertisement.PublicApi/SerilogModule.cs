using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using IConfiguration = AutoMapper.Configuration.IConfiguration;

namespace Advertisement.PublicApi
{
    public static class SerilogModule
    {
        public static IServiceCollection AddSerilogModule(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq("http://62.109.26.248:5341")
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            services.AddSingleton(Log.Logger);
            return services;
        }
    }
}