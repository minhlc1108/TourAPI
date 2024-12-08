
using Microsoft.Identity.Client;
using TourAPI.Dtos.BookingDetail;

namespace TourAPI.Dtos.Booking
{
    public class CreateBookingReqDto
    {
        public decimal TotalPrice { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TourScheduleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Time { get; set; }
        public string Name { get; set; }
        public int Sex { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public List<CreateBookingDetailAndCustomer> Participants { get; set; }
    }
}
