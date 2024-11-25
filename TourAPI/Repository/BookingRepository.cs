using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Dtos.Booking;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Data;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<(List<Booking>, int totalCount)> GetAllAsync(BookingQueryObject query)
        {
            var bookings = _context.Bookings
        .Include(b => b.Customer)
        .Include(b => b.TourSchedule)
        .Include(b => b.BookingDetails)
        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Id))
            {
                bookings = bookings.Where(b => b.Id.ToString().Contains(query.Id));
            }

            if (!string.IsNullOrWhiteSpace(query.CustomerId))
            {
                bookings = bookings.Where(b => b.CustomerId.ToString().Contains(query.CustomerId));
            }

            if (!string.IsNullOrWhiteSpace(query.TourScheduleId))
            {
                bookings = bookings.Where(b => b.TourScheduleId.ToString().Contains(query.TourScheduleId));
            }

            if (query.Status.HasValue)
            {
                bookings = bookings.Where(b => b.Status == query.Status);
            }

            if (query.StartDate.HasValue)
            {
                bookings = bookings.Where(b => b.Time >= query.StartDate.Value);
            }

            if (query.EndDate.HasValue)
            {
                bookings = bookings.Where(b => b.Time <= query.EndDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = query.IsDescending
                        ? bookings.OrderByDescending(b => b.Time)
                        : bookings.OrderBy(b => b.Time);
                }
                else if (query.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = query.IsDescending
                        ? bookings.OrderByDescending(b => b.Id)
                        : bookings.OrderBy(b => b.Id);
                }
            }

            int totalCount = await bookings.CountAsync();

            if (query.PageNumber > 0 && query.PageSize > 0)
            {
                var skipNumber = (query.PageNumber - 1) * query.PageSize;
                bookings = bookings.Skip(skipNumber).Take(query.PageSize);
            }

            var pagedBookings = await bookings.ToListAsync();
            return (pagedBookings, totalCount);
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return booking;
            }

            return null;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return false;
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
