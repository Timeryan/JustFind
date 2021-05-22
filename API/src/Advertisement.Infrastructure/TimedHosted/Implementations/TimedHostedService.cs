using System;
using System.Threading;
using System.Threading.Tasks;
using Advertisement.Application.Services.Ad.Interfaces;
using Advertisement.Application.Services.Photo.Implementations;
using Advertisement.Application.Services.Photo.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Advertisement.Infrastructure.TimedHosted.Implementations
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IPhotoService _photoService;
        private readonly IAdService _adService;

        public TimedHostedService(IServiceScopeFactory factory)
        {
            _photoService = factory.CreateScope().ServiceProvider.GetRequiredService<IPhotoService>();
            _adService = factory.CreateScope().ServiceProvider.GetRequiredService<IAdService>();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, 
                TimeSpan.FromSeconds(3600));

            return Task.CompletedTask;
        }
    
        private void DoWork(object state)
        {
             _photoService.DeleteAllPhotoWithOutAdOwner();
             _adService.DeleteAdAfterMonth();
        }
    
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
    
            return Task.CompletedTask;
        }
    
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}