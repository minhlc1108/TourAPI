using System;
using System.Collections.Generic;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Dtos.Customer;
using TourAPI.Dtos.Tour;
using TourAPI.Models;

namespace TourAPI.Dtos.Booking
{
    public class BookingDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int Status { get; set; }
        public DateTime Time { get; set; }
        public int TourScheduleId { get; set; }
        public int CustomerId { get; set; }
        public TourAPI.Models.TourSchedule? TourSchedule { get; set; }
        public TourAPI.Models.Customer? Customer { get; set; }
        public List<BookingDetailDto>? BookingDetails { get; set; }
    }
}
