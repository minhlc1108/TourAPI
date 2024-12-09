using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Dtos.Bookings;
using TourAPI.Helpers;

namespace TourAPI.Interfaces.Service
{
    public interface IBookingService
    {
        Task<BookingResultDto> GetAllAsync(BookingQueryObject query);

        Task<BookingDto?> GetByIdAsync(int id);

        Task<BookingDto?> CreateAsync(CreateBookingReqDto bookingDto);

        Task<BookingDto?> UpdateAsync(int id, UpdateBookingReqDto bookingDto);

        Task<BookingDto?> DeleteById(int id);
        Task<BookingDto> CreateBookingAsync(CreateBookingReqDto createBookingReqDto);
        Task<BookingResDto> GetBookingDetailsAsync(int id);
        Task updateBookingStatus(int bookingId, int status);
        Task UpdateExpiredBookingStatusAsync();
        Task CheckBeforeCreatePayment(int bookingId);
    }
}
