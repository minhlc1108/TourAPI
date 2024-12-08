using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Bookings;
using TourAPI.Exceptions;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITourScheduleRepository _tourScheduleRepository;
        public BookingService(IBookingRepository bookingRepository, ICustomerRepository customerRepository, IAccountRepository accountRepository, ITourScheduleRepository tourScheduleRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _tourScheduleRepository = tourScheduleRepository;
        }

        public async Task<BookingDto> CreateBookingAsync(CreateBookingReqDto createBookingReqDto)
        {
            var rs = await _tourScheduleRepository.CheckAvailable(createBookingReqDto.TourScheduleId, createBookingReqDto.Customers.Count);
            if (rs == false)
            {
                throw new BadHttpRequestException("Không còn chỗ trống");
            }
            var customer = new Customer();
            if (createBookingReqDto.CustomerId == null)
            {
                var account = await _accountRepository.GetAccountByEmailAsync(createBookingReqDto.Email);
                if (account == null)
                {
                    customer = new Customer
                    {
                        Name = createBookingReqDto.Name,
                        Phone = createBookingReqDto.Phone,
                        Email = createBookingReqDto.Email
                    };
                    await _customerRepository.AddCustomerAsync(customer);
                }
                else
                {
                    account.PhoneNumber = createBookingReqDto.Phone;
                    await _accountRepository.UpdateAccountAsync(account);
                    customer = await _customerRepository.GetCustomerByAccountIdAsync(account.Id);
                    if (customer != null)
                    {
                        customer.Name = createBookingReqDto.Name;
                        await _customerRepository.UpdateCustomerAsync(customer);
                    }
                    else
                    {
                        customer = new Customer
                        {
                            AccountId = account.Id,
                            Name = createBookingReqDto.Name,
                        };
                        await _customerRepository.AddCustomerAsync(customer);
                    }
                }


            }
            var bookingDetails = new List<BookingDetail>();

            foreach (var bookingDetailReqDto in createBookingReqDto.Customers)
            {
                var customerModel = bookingDetailReqDto.ToCustomerFromCreateBookingDetailReqDto();
                await _customerRepository.AddCustomerAsync(customerModel);
                var bookingDetailModel = bookingDetailReqDto.ToBookingDetailFromCreateBookingDetailReqDto(customerModel.Id);
                bookingDetails.Add(bookingDetailModel);
            }


            createBookingReqDto.CustomerId = customer.Id;
            var booking = createBookingReqDto.ToBookingFromCreateBookingReqDto(bookingDetails);

            await _bookingRepository.CreateAsync(booking);
            return booking.ToBookingDTO();
        }

        public async Task<BookingResDto> GetBookingDetailsAsync(int id)
        {
            var booking = await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
            {
                throw new BadHttpRequestException("Không tìm thấy booking");
            }

            var orderer = await _customerRepository.GetCustomerByIdAsync(booking.CustomerId);

            if (orderer.Email == null || orderer.Phone == null)
            {
                var account = await _accountRepository.GetAccountByIdAsync(orderer.AccountId);
                orderer.Email = account.Email;
                orderer.Phone = account.PhoneNumber;
            }

            var tourSchedule = await _tourScheduleRepository.GetTourScheduleAsync(booking.TourScheduleId);

            return new BookingResDto
            {
                Id = booking.Id,
                TotalPrice = booking.TotalPrice,
                PriceDiscount = booking.PriceDiscount,
                Time = booking.Time,
                TourSchedule = tourSchedule.ToTourScheduleDto(),
                Customer = orderer.ToOrdererDto(),
                PaymentMethod = booking.PaymentMethod,
                Status = booking.Status,
                BookingDetails = booking.BookingDetails.Select(bd => bd.ToBookingDetailDtoFromBookingDetail()).ToList()
            };

        }

        public async Task updateBookingStatus(int bookingId, int status)
        {
            await _bookingRepository.updateBookingStatus(bookingId, status);
        }

        public async Task UpdateExpiredBookingStatusAsync()
        {
            var expiredBookings = await _bookingRepository.GetExpiredBookingsAsync();
            foreach (var booking in expiredBookings)
            {
                booking.Status = 2;
                await _tourScheduleRepository.IncreaseAvailableSlot(booking.TourScheduleId, booking.BookingDetails.Count);
            }
            await _bookingRepository.UpdateBookingsAsync(expiredBookings);
        }

        public async Task CheckBeforeCreatePayment(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
            {
                throw new NotFoundException("Không tìm thấy booking");
            }
            switch(booking.Status)
            {
                case 1:
                    throw new BadHttpRequestException("Booking đã thanh toán");
                case 2:
                    throw new BadHttpRequestException("Booking đã bị hủy");
            }
        }
    }
}