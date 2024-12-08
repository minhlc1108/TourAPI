using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(int id);
        Task updateBookingStatus(int bookingId, int status);
    }
}