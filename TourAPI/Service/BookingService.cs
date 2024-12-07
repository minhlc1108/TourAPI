using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Bookings;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;

namespace TourAPI.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        public BookingService(IBookingRepository bookingRepository, ICustomerRepository customerRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
        }
        public Task CreateBookingAsync(CreateBookingReqDto createBookingReqDto)
        {
            // if(createBookingReqDto.CustomerId == null)
            // {
            //     _customerRepository.CreateCustomerAsync(new CreateCustomerReqDto
            //     {
            //         Name = createBookingReqDto.Name,
            //         Phone = createBookingReqDto.Phone,
            //         Email = createBookingReqDto.Email,
            //         Address = createBookingReqDto.Address
            //     });
            // }
            throw new NotImplementedException();
        }
   }
}