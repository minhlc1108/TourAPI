using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Booking;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Dtos.Bookings;
using TourAPI.Dtos.Customer;
using TourAPI.Helpers;
using TourAPI.Interfaces.Service;
using TourAPI.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;
        private readonly IBookingDetailService _bookingDetailService;

        public BookingController(IBookingService bookingService, ICustomerService customerService, IBookingDetailService bookingDetailService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _bookingDetailService = bookingDetailService;
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

        [HttpPost]
        [Route("create-booking-customer")]
        public async Task<IActionResult> CreateBookingAndCustomer([FromBody] CreateBookingReqDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerDto = new CreateCustomerReqDto
            {
                Name = bookingDto.Name,
                Sex = bookingDto.Sex,
                Address = bookingDto.Address,
                Birthday = bookingDto.Birthday,
                Email = bookingDto.Email,
                PhoneNumber = bookingDto.PhoneNumber
            };

            var createdCustomer = await _customerService.Create(customerDto);
            if (createdCustomer == null)
            {
                return StatusCode(500, "Không th? t?o khách hàng");
            }

            bookingDto.CustomerId = createdCustomer.Id;

            var createdBookingDto = await _bookingService.CreateAsync(bookingDto);
            if (createdBookingDto == null)
            {
                return StatusCode(500, "Không th? t?o booking");
            }

            foreach (var participant in bookingDto.Participants)
            {
                var customerDtoItem = new CreateCustomerReqDto
                {
                    Name = participant.Name,
                    Sex = participant.Sex,
                    Address = participant.Address,
                    Birthday = participant.Birthday,
                    Email = participant.Email,
                    PhoneNumber = participant.PhoneNumber
                };
                var createdCustomerItem = await _customerService.Create(customerDtoItem);
                var bookingDetailDto = new CreateBookingDetailReqDto
                {
                    BookingId = createdBookingDto.Id,
                    CustomerId = createdCustomerItem.Id,
                    Price = participant.Price,
                    Status = 1
                };
                await _bookingDetailService.CreateAsync(bookingDetailDto);
            }

            return CreatedAtAction(nameof(GetById), new { id = createdBookingDto.Id }, createdBookingDto);
        }
        [HttpPut]
        [Route("update-booking-customer/{id:int}")]
        public async Task<IActionResult> UpdateBookingAndCustomer([FromRoute] int id, [FromBody] UpdateBookingReqDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customerDto = new UpdateCustomerReqDto
            {
                Id = bookingDto.CustomerId,
                Name = bookingDto.Name,
                Sex = bookingDto.Sex,
                Address = bookingDto.Address,
                Birthday = bookingDto.Birthday,
                Email = bookingDto.Email,
                PhoneNumber = bookingDto.PhoneNumber
            };

            var updatedCustomer = await _customerService.Update(customerDto);
            if (updatedCustomer == null)
            {
                return StatusCode(500, "Không th? c?p nh?t khách hàng");
            }

            var updatedBookingDto = await _bookingService.UpdateAsync(id, bookingDto);
            if (updatedBookingDto == null)
            {
                return StatusCode(500, "Không th? c?p nh?t booking");
            }

            var deleteDetailsResult = await _bookingDetailService.DeleteByIdAsync(updatedBookingDto.Id);
            if (!deleteDetailsResult)
            {
                return StatusCode(500, "Không th? xóa chi ti?t booking c?");
            }

            foreach (var participant in bookingDto.Participants)
            {
                if (participant.CustomerId != null)
                {
                   await _customerService.Delete(participant.CustomerId);
                  
                }

                var customerDtoItem = new CreateCustomerReqDto
                {
                    Name = participant.Name,
                    Sex = participant.Sex,
                    Address = participant.Address,
                    Birthday = participant.Birthday,
                    Email = participant.Email,
                    PhoneNumber = participant.PhoneNumber
                };

                var createdCustomerItem = await _customerService.Create(customerDtoItem);
                if (createdCustomerItem == null)
                {
                    return StatusCode(500, "Không th? t?o khách hàng m?i");
                }

                var bookingDetailDto = new CreateBookingDetailReqDto
                {
                    BookingId = updatedBookingDto.Id,
                    CustomerId = createdCustomerItem.Id,
                    Price = participant.Price,
                    Status = 1
                };
                await _bookingDetailService.CreateAsync(bookingDetailDto);
            }

            return Ok(updatedBookingDto);
        }
        [HttpPut]
        [Route("update-status/{id:int}")]
        public async Task<IActionResult> UpdateBookingStatus([FromRoute] int id, [FromBody] UpdateBookingStatusReqDto statusDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookingDto = await _bookingService.GetByIdAsync(id);
            if (bookingDto == null)
            {
                return NotFound($"Booking with ID {id} not found.");
            }

            bookingDto.Status = statusDto.Status;

            var updatedBookingDto = await _bookingService.UpdateStatusAsync(id, statusDto);
            if (updatedBookingDto == null)
            {
                return StatusCode(500, "Không thể cập nhật trạng thái booking.");
            }

            return Ok(updatedBookingDto);
        }

    }
}
