using System.ComponentModel.DataAnnotations.Schema;

namespace TourAPI.Dtos.BookingDetail
{
    public class BookingDetailToanDto
    {

        public int Id { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public string? Detail { get; set; } = String.Empty;
        public int Status { get; set; }
        public TourAPI.Models.Booking? Booking { get; set; }
        public TourAPI.Models.Customer? Customer { get; set; }
    }
}
