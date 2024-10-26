using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("BookingDetails")]
    public class BookingDetail
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int Price { get; set; }
        public string Detail { get; set; }
        public int Status { get; set; }
        public Booking Booking {get; set;}
        public Customer Customer {get; set;}
    }
}