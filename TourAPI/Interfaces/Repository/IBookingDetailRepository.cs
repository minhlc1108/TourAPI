using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IBookingDetailRepository
    {
        Task<(List<BookingDetail>, int totalCount)> GetAllAsync(int bookingId);

        Task<BookingDetail?> GetByIdAsync(int id);

        Task<BookingDetail> CreateAsync(BookingDetail bookingDetail);

        Task<BookingDetail?> UpdateAsync(BookingDetail bookingDetail);

        Task<bool> DeleteByIdAsync(int id);
    }
}
