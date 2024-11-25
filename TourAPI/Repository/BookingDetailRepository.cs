using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Interfaces.Repository;
using TourAPI.Models;
using TourAPI.Data;

namespace TourAPI.Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly ApplicationDBContext _context;

        public BookingDetailRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(List<BookingDetail>, int totalCount)> GetAllAsync(int bookingId)
        {
            var bookingDetails = _context.BookingDetails
                .Where(bd => bd.BookingId == bookingId)
                .Include(bd => bd.Customer)
                .AsQueryable();

            int totalCount = await bookingDetails.CountAsync();

            var bookingDetailList = await bookingDetails.ToListAsync();
            return (bookingDetailList, totalCount);
        }

        public async Task<BookingDetail?> GetByIdAsync(int id)
        {
            return await _context.BookingDetails
                .FirstOrDefaultAsync();
        }

        public async Task<BookingDetail> CreateAsync(BookingDetail bookingDetail)
        {
            await _context.BookingDetails.AddAsync(bookingDetail);
            await _context.SaveChangesAsync();
            return bookingDetail;
        }

        public async Task<BookingDetail?> UpdateAsync(BookingDetail bookingDetail)
        {
            _context.BookingDetails.Update(bookingDetail);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return bookingDetail;
            }

            return null;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return false;
            }

            _context.BookingDetails.Remove(bookingDetail);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
