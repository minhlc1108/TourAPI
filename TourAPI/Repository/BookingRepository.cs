using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Exceptions;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;

namespace TourAPI.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDBContext _context;

        public BookingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.Include(b => b.BookingDetails).ThenInclude(bd => bd.Customer).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task updateBookingStatus(int bookingId, int status)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                throw new NotFoundException("Không tìm thấy booking");
            }
            booking.Status = status;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}