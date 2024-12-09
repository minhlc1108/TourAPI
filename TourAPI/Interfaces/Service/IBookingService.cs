using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Bookings;

namespace TourAPI.Interfaces.Service
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(CreateBookingReqDto createBookingReqDto);
        Task<BookingResDto> GetBookingDetailsAsync(int id);
        Task updateBookingStatus(int bookingId, int status);
        Task UpdateExpiredBookingStatusAsync();
        Task CheckBeforeCreatePayment(int bookingId);
    }
}