using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Exceptions;
using TourAPI.Helpers;
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

        public async Task DeleteByIdAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<(List<Booking>, int totalCount)> GetAllAsync(BookingQueryObject queryObject)
        {
            var bookings = _context.Bookings.Include(b => b.TourSchedule).AsQueryable();

            if (queryObject.Status != null)
            {
                bookings = bookings.Where(b => b.Status == queryObject.Status);
            }

            if (queryObject.FromDate != null && queryObject.ToDate != null)
            {
                bookings = bookings.Where(b => b.Time.Date >= queryObject.FromDate.Value.Date && b.Time <= queryObject.ToDate.Value.Date.AddDays(1));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.Id) : bookings.OrderBy(b => b.Id);
                }

                if (queryObject.SortBy.Equals("CustomerId", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.CustomerId) : bookings.OrderBy(b => b.CustomerId);
                }

                if (queryObject.SortBy.Equals("Time", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.Time) : bookings.OrderBy(b => b.Time);
                }

                if (queryObject.SortBy.Equals("TotalPrice", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.TotalPrice) : bookings.OrderBy(b => b.TotalPrice);
                }

                if (queryObject.SortBy.Equals("PriceDiscount", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.PriceDiscount) : bookings.OrderBy(b => b.PriceDiscount);
                }

                if (queryObject.SortBy.Equals("AdultCount", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.AdultCount) : bookings.OrderBy(b => b.AdultCount);
                }

                if (queryObject.SortBy.Equals("ChildCount", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = queryObject.IsDecsending ? bookings.OrderByDescending(b => b.ChildCount) : bookings.OrderBy(b => b.ChildCount);
                }
            }
            // bookings = bookings.Where(c => c.Status == bookings.Status);
            int totalCount = await bookings.CountAsync();
            var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
            var pagedBookings = await bookings.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
            return (
                pagedBookings,
                totalCount
            );
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Bookings.Include(b => b.BookingDetails).ThenInclude(bd => bd.Customer).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetExpiredBookingsAsync()
        {
            return await _context.Bookings.Where(b => b.Status == 0 && DateTime.Now >= b.Time.AddHours(8)).ToListAsync();
        }

        public Task UpdateBookingsAsync(List<Booking> bookings)
        {
            _context.Bookings.UpdateRange(bookings);
            return _context.SaveChangesAsync();
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