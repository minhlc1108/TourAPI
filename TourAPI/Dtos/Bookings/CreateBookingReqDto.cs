using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.BookingDetail;

namespace TourAPI.Dtos.Bookings
{
    public class CreateBookingReqDto
    {
        public int? CustomerId {get; set;}
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
        public int? PriceDiscount {get; set;}
        public int? PromotionId {get; set;}
        public int TotalPrice {get; set;}
        public string PaymentMethod {get; set;}
        public int Status {get; set;} = 0;
        public List<CreateBookingDetailReqDto> Customers {get; set;}
    }
}