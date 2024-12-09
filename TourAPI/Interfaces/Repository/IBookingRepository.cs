using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IBookingRepository
    {
        Task<(List<Booking>, int totalCount)> GetAllAsync(BookingQueryObject query);

        Task<Booking?> GetByIdAsync(int id);

        Task<Booking> CreateAsync(Booking booking);

        Task<Booking?> UpdateAsync(Booking booking);

        Task<bool> DeleteByIdAsync(int id);
        Task updateBookingStatus(int bookingId, int status);
        Task<List<Booking>> GetExpiredBookingsAsync();
        Task UpdateBookingsAsync(List<Booking> bookings);
    }
}
