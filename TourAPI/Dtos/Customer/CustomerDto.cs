using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Dtos.Tour;

namespace TourAPI.Dtos.Customer
{
    public class CustomerDto
    {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Sex {get; set;}
    public string? Address { get; set; }= string.Empty;
    public DateTime  Birthday { get; set; }
    public int Status {get; set;}
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password {get;set;}
    public List<BookingDto>? Bookings {get;set;} 

    // public List<OrderDto> Orders { get; set; }
    }
}