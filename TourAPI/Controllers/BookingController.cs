using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Booking;
using TourAPI.Helpers;
using TourAPI.Dtos.Bookings;
using TourAPI.Dtos.VNPay;
using TourAPI.Interfaces.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IVNPayService _vnPayService;

        public BookingController(IBookingService bookingService, IVNPayService vnPayService)
        {
            _bookingService = bookingService;
            _vnPayService = vnPayService;
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
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingReqDto createBookingReqDto)
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
            // Create booking
            var result = await _bookingService.CreateBookingAsync(createBookingReqDto);
            return Ok(result);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetBookingDetails([FromRoute] int id)
        {
            // Get booking detail
            var result = await _bookingService.GetBookingDetailsAsync(id);
            return Ok(result);
        }

        [HttpPost("payment/vnpay")]
        public IActionResult GetPaymentVNPay([FromBody] VNPayReqDto paymentReqDto)
        {
            // Payment
            var result = _vnPayService.CreatePaymentUrl(paymentReqDto, HttpContext);
            return Ok(result);
        }

        [HttpGet("payment/vnpay/callback")]
        public async Task<IActionResult> PaymentVNPayCallback()
        {
            // Payment callback
            var collections = HttpContext.Request.Query;
            var result = _vnPayService.PaymentExecute(collections);
            if (result.Success)
            {
                await _bookingService.updateBookingStatus(Int32.Parse(result.OrderId), 1);
                // Thanh toán thành công, chuyển hướng về trang thành công
                return Redirect("http://localhost:5173/payment-booking/" + result.OrderId);
            }
            else
            {
                // Thanh toán thất bại, chuyển hướng về trang thất bại
                return Redirect("http://localhost:5173/payment-booking/" + result.OrderId + "+?error=true");
            }
        }

        [HttpGet("check-before-create-payment/{id}")]
        public async Task<IActionResult> CheckBeforeCreatePayment([FromRoute] int id)
        {
            await _bookingService.CheckBeforeCreatePayment(id);
            return Ok();
        }
    }
}
