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
                TourSchedule = booking.TourSchedule,
                
                // Tours = booking.Tours.Select(t => t.ToTourDTO()).ToList()
            };
        }
    }
}