using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Tour;
using TourAPI.Models;

namespace TourAPI.Dtos.Bookings
{
    public class BookingDto
    {   
         public int Id {get; set;} 

        public int TotalPrice {get; set;}
        public int AdultCount {get; set;}
        public int ChildCount {get; set;}
        public int Status {get; set;}
        public DateTime Time {get; set;}
        public int TourScheduleId {get; set;}
        // public int CustomerId {get; set;}
        public TourScheduleDto? TourSchedule {get; set;}
        // public List<BookingDetail> BookingDetails {get;set;} = new List<BookingDetail>();  
        
        // public List<TourSchedule> BookingDetails {get;set;} = new List<TourSchedule>();  

    }
}