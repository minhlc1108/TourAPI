using System;
using System.Collections.Generic;
using TourAPI.Dtos.Category;

namespace TourAPI.Dtos.Booking
{
    public class BookingResultDto
    {
        public List<BookingDto> Bookings { get; set; }
        public int Total { get; set; }
    }
}
