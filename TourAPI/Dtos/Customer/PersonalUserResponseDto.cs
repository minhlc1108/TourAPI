using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TourAPI.Dtos.Customer
{
    public class PersonalUserResponseDto
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public int Sex { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public DateTime Birthday { get; set; }

        public string Password {get;set;}

        // public List<BookingDto>? Bookings {get;set;} 


    }
}