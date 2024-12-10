using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(int id);
        Task updateBookingStatus(int bookingId, int status);
        Task<List<Booking>> GetExpiredBookingsAsync();
        Task<(List<Booking>, int totalCount)> GetAllAsync(BookingQueryObject queryObject);
        Task UpdateBookingsAsync(List<Booking> bookings);
        Task DeleteByIdAsync(Booking booking);
    }
}