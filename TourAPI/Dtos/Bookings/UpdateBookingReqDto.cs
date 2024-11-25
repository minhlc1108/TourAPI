namespace TourAPI.Dtos.Booking
{
    public class UpdateBookingReqDto
    {
        public decimal TotalPrice { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public int Status { get; set; }
        public int TourScheduleId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Time { get; set; }
    }
}
