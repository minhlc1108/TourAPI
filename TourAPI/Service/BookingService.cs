using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Booking;
using TourAPI.Exceptions;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using TourAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Models;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Bookings;

namespace TourAPI.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
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
        public BookingService(IBookingRepository bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        public async Task<BookingDto?> CreateAsync(CreateBookingReqDto bookingDto)
        {
            var bookingModel = bookingDto.ToBookingModel();
            var createdBooking = await _bookingRepo.CreateAsync(bookingModel);
            return createdBooking.ToBookingResultDto();
        }

        public async Task<BookingDto?> DeleteById(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            await _bookingRepo.DeleteByIdAsync(id);
            return booking.ToBookingResultDto();
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
                await _tourScheduleRepository.IncreaseAvailableSlot(booking.TourScheduleId, booking.AdultCount + booking.ChildCount);
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
        
        public async Task<BookingResultDto> GetAllAsync(BookingQueryObject query)
        {
            var (bookings, totalCount) = await _bookingRepo.GetAllAsync(query);
            var bookingsDto = bookings.Select(c => c.ToBookingResultDto()).ToList();
            return new BookingResultDto
            {
                Bookings = bookingsDto,
                Total = totalCount
            };

        }



        public async Task<BookingDto?> GetByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            return booking.ToBookingResultDto();
        }

        public async Task<BookingDto?> UpdateAsync(int id, UpdateBookingReqDto bookingDto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            booking.UpdateBookingFromDto(bookingDto);
            var updatedBooking = await _bookingRepo.UpdateAsync(booking);
            return updatedBooking.ToBookingResultDto();
        }

        Task<BookingResDto> IBookingService.GetBookingDetailsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
