using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("Customers")]
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Sex { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        public int Status { get; set; }

        public int? RelatedCustomerId { get; set; }
        public Customer? RelatedCustomer { get; set; }
        public string? AccountId { get; set; }
        public Account? Account { get; set; }

        public List<Booking> Bookings {get; set;} = new List<Booking>();
        public List<BookingDetail> BookingDetails {get; set;} = new List<BookingDetail>();
    }
}