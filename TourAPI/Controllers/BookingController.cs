using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Bookings;
using TourAPI.Dtos.VNPay;
using TourAPI.Helpers;
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

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingReqDto createBookingReqDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Create booking
            var result = await _bookingService.CreateBookingAsync(createBookingReqDto);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBooking([FromRoute] int id) {
            await _bookingService.DeleteBookingAsync(id);
            return Ok();
        }
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetBookingDetails([FromRoute] int id)
        {
            // Get booking detail
            var result = await _bookingService.GetBookingDetailsAsync(id);
            return Ok(result);
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatusBooking([FromBody] UpdateStatusReqDto req)
        {
            // Get booking detail
            await _bookingService.UpdateBookingStatus(req.Id, req.Status);
            return Ok();
        }

        [HttpGet]
        public  Task<IActionResult> GetAll([FromQuery] BookingQueryObject queryObject)
        {
            // Get booking
            var result =  _bookingService.GetAll(queryObject);
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
                await _bookingService.UpdateBookingStatus(Int32.Parse(result.OrderId), 1);
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