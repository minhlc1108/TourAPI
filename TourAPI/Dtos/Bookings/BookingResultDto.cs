using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Bookings
{
    public class BookingResultDto
    {
        public List<BookingDto> Bookings { get; set; } = new List<BookingDto>();
         public int Total {get; set;}
    }
}