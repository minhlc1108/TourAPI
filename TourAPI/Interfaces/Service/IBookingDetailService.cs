using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Dtos.BookingDetail;

namespace TourAPI.Interfaces.Service
{
    public interface IBookingDetailService
    {
        Task<List<BookingDetailDto>> GetAllByBookingIdAsync(int bookingId);

        Task<BookingDetailDto?> GetByIdAsync(int id);

        Task<BookingDetailDto?> CreateAsync(CreateBookingDetailReqDto bookingDetailDto);

        Task<BookingDetailDto?> UpdateAsync(int id, UpdateBookingDetailReqDto bookingDetailDto);

        Task<bool> DeleteByIdAsync(int id);
    }
}
