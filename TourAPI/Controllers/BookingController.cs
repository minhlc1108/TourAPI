using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Bookings;
using TourAPI.Interfaces.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpPost] 
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingReqDto createBookingReqDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Create booking
            await _bookingService.CreateBookingAsync(createBookingReqDto);
            return Ok();
        }
    }
}