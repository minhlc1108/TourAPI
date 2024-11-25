namespace TourAPI.Dtos.BookingDetail
{
    public class CreateBookingDetailReqDto
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public string? Detail { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
