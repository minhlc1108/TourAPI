using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.BookingDetail
{
    public class CreateBookingDetailReqDto
    {
        public string Name {get; set;}
        public DateTime Birthday {get; set;}
        public int Sex {get; set;}
        public int Price {get; set;}
        public int Status { get; set; } = 1;
    }
}