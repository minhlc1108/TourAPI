using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Models
{
    [Table("TourSchedules")]
    public class TourSchedule
    {
        public int Id { get; set; }
        public string? Name { get; set; } = String.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Remain { get; set; }
        public int PriceAdult { get; set; }
        public int PriceChild { get; set; }
        public int Status { get; set; } = 1;
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public List<Booking> Bookings {get; set;} = new List<Booking>();
    }
}