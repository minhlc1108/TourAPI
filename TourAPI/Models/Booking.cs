using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{   
    [Table("Bookings")]
    public class Booking
    {
        public int Id {get; set;} 

        public int TotalPrice {get; set;}
        public int AdultCount {get; set;}
        public int ChildCount {get; set;}
        public int Status {get; set;}
        public DateTime Time {get; set;}
        public int TourScheduleId {get; set;}
        public int CustomerId {get; set;}
        public TourSchedule? TourSchedule {get; set;}
        public Customer? Customer {get;set;}
        public List<BookingDetail> BookingDetails {get;set;} = new List<BookingDetail>();  
    }
}