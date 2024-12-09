using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Interfaces.Service;

namespace TourAPI.Service
{
    public class BookingStatusUpdateService : BackgroundService
    {
         private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromHours(1);
        public BookingStatusUpdateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
             while (!stoppingToken.IsCancellationRequested){
                using (var scope = _serviceScopeFactory.CreateScope())
            {
                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                await bookingService.UpdateExpiredBookingStatusAsync();
            }
                await Task.Delay(_interval, stoppingToken);
             }
        

        }
    }
}