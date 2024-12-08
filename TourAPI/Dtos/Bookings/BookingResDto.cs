using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Dtos.Customer;
using TourAPI.Dtos.Tour;

namespace TourAPI.Dtos.Bookings
{
    public class BookingResDto
    {
        public int Id { get; set; }

        public int TotalPrice { get; set; }
        public int PriceDiscount { get; set; }
        public string PaymentMethod { get; set; }
        public TourScheduleDto TourSchedule { get; set; }
        public OrdererDto Customer { get; set; }
        public List<BookingDetailDto> BookingDetails { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
}
}