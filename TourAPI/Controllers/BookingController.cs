using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Booking;
using TourAPI.Helpers;
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

        // GET: api/booking
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BookingQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingResultDto = await _bookingService.GetAllAsync(query);
            return Ok(bookingResultDto);
        }

        // GET: api/booking/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingDto = await _bookingService.GetByIdAsync(id);
            return Ok(bookingDto);
        }

        // POST: api/booking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingReqDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBookingDto = await _bookingService.CreateAsync(bookingDto);
            return CreatedAtAction(nameof(GetById), new { id = createdBookingDto.Id }, createdBookingDto);
        }

        // PUT: api/booking/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookingReqDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBookingDto = await _bookingService.UpdateAsync(id, bookingDto);
            return Ok(updatedBookingDto);
        }

        // DELETE: api/booking/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deletedBookingDto = await _bookingService.DeleteById(id);
            return Ok(deletedBookingDto);
        }
    }
}
