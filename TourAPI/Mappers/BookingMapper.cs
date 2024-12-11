using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Bookings;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class BookingMapper
    {
         public static BookingDto ToBookingDTO(this Booking booking)
        {
            return new BookingDto
            {
                Id = booking.Id,
                TotalPrice  = booking.TotalPrice,
                AdultCount = booking.AdultCount,
                ChildCount = booking.ChildCount,
                Status = booking.Status,
                Time = booking.Time,
                TourScheduleId = booking.TourScheduleId,
                TourSchedule = booking.TourSchedule?.ToTourScheduleDto(),
                CustomerId = booking.CustomerId,
                PaymentMethod = booking.PaymentMethod,
                PriceDiscount = booking.PriceDiscount,
                PromotionId = booking.PromotionId
                
                // TTours = booking.Tours.Select(t => t.ToTourDTO()).ToList()
            };
        }

        public static Booking ToBookingFromCreateBookingReqDto(this CreateBookingReqDto createBookingReqDto, List<BookingDetail> bookingDetails)
        {

            return new Booking
            {
                TourScheduleId = createBookingReqDto.TourScheduleId,
                AdultCount = createBookingReqDto.AdultCount,
                ChildCount = createBookingReqDto.ChildCount,
                Time = createBookingReqDto.Time,
                PriceDiscount = createBookingReqDto.PriceDiscount,
                PromotionId = createBookingReqDto.PromotionId,
                TotalPrice = createBookingReqDto.TotalPrice,
                PaymentMethod = createBookingReqDto.PaymentMethod,
                Status = createBookingReqDto.Status,
                CustomerId = createBookingReqDto.CustomerId.Value,
                BookingDetails = bookingDetails
            };
            }
       
    }
}