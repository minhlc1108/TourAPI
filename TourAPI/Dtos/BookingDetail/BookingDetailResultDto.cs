using TourAPI.Dtos.Booking;

namespace TourAPI.Dtos.BookingDetail
{
    public class BookingDetailResultDto
    {
        public List<BookingDetailDto> BookingDetails { get; set; }
        public int Total { get; set; }
    }
}
