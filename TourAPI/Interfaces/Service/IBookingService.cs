using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Bookings;

namespace TourAPI.Interfaces.Service
{
    public interface IBookingService
    {
        Task CreateBookingAsync(CreateBookingReqDto createBookingReqDto);
    }
}