using TourAPI.Dtos.Booking;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class BookingMapper
    {

        public static BookingDto ToBookingResultDto(this Booking booking)
        {
            if (booking == null)
            {
                return null;
            }

            return new BookingDto
            {
                Id = booking.Id,
                TotalPrice = booking.TotalPrice,
                AdultCount = booking.AdultCount,
                ChildCount = booking.ChildCount,
                Status = booking.Status,
                Time = booking.Time,
                TourScheduleId = booking.TourScheduleId,
                CustomerId = booking.CustomerId,
                Customer = booking.Customer,
                TourSchedule = booking.TourSchedule,
                BookingDetails = booking.BookingDetails?.Select(bd => bd.ToBookingDetailDto()).ToList()
            };
        }


        public static Booking ToBookingModel(this CreateBookingReqDto bookingDto)
        {
            return new Booking
            {
                TotalPrice = (int)bookingDto.TotalPrice,
                AdultCount = bookingDto.AdultCount,
                ChildCount = bookingDto.ChildCount,
                Status = bookingDto.Status,
                Time = bookingDto.Time,
                TourScheduleId = bookingDto.TourScheduleId,
                CustomerId = bookingDto.CustomerId
            };
        }

        public static void UpdateBookingFromDto(this Booking booking, UpdateBookingReqDto bookingDto)
        {
            booking.TotalPrice = (int)bookingDto.TotalPrice;
            booking.AdultCount = bookingDto.AdultCount;
            booking.ChildCount = bookingDto.ChildCount;
            booking.Status = bookingDto.Status;
            booking.Time = bookingDto.Time;
            booking.TourScheduleId = bookingDto.TourScheduleId;
            booking.CustomerId = bookingDto.CustomerId;
        }
    }
}
