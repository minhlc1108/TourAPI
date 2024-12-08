namespace TourAPI.Dtos.BookingDetail
{
    public class UpdateBookingDetailAndCustomer
    {
        public int CustomerId { get; set; }
        public int BookingId { get; set; }
        public string? Name { get; set; }
        public int Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int Price { get; set; }
        public string? Detail { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}
