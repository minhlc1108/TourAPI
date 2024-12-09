
using TourAPI.Dtos.BookingDetail;

namespace TourAPI.Dtos.Booking
{
    public class CreateBookingReqDto
    {
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name {get; set;}
        [Required]
        [RegularExpression(@"^0\d{9,10}$")]   
        public string Phone {get; set;}
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
        [MaxLength(255)]
        public string Email {get; set;}
        public string? Address {get; set;}
        [Required]
        public int TourScheduleId {get; set;}
        [Required]
        public int AdultCount {get; set;}
        [Required]
        public int ChildCount {get; set;}
        public DateTime Time {get; set;} = DateTime.Now;
        public int PriceDiscount {get; set;} = 0;
        public int? PromotionId {get; set;}
        public string PaymentMethod {get; set;}
        public int Status {get; set;} = 0;
        public List<CreateBookingDetailReqDto> Customers {get; set;}
    }
}
