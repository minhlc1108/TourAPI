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
using TourAPI.Dtos.BookingDetail;
using TourAPI.Repository;
using TourAPI.Dtos.Bookings;

namespace TourAPI.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IBookingDetailRepository _bookingDetailRepo;
        private readonly ICustomerRepository _customerRepo;


        public BookingService(IBookingRepository bookingRepo, ICustomerRepository customerRepo, IBookingDetailRepository bookingDetailRepo)
        {
            _bookingRepo = bookingRepo;
            _customerRepo = customerRepo;
            _bookingDetailRepo = bookingDetailRepo;
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
        public async Task<BookingDetailDto> GetByBookingIdAndCustomerIdAsync(int bookingId, int customerId)
        {
            var bookingDetail = await _bookingDetailRepo.GetByBookingIdAndCustomerIdAsync(bookingId, customerId);
            if (bookingDetail == null)
            {
                return null;
            }

            return new BookingDetailDto
            {
                BookingId = bookingDetail.BookingId,
                CustomerId = (int)bookingDetail.CustomerId,
                Price = bookingDetail.Price,
                Status = bookingDetail.Status
            };
        }

        public async Task<bool> UpdateStatusAsync(int id, UpdateBookingStatusReqDto statusDto)
        {
            var booking = await _bookingRepo.GetByIdAsync(id);

            if (booking == null)
            {
                throw new NotFoundException("Booking not found");
            }

            var updatedBooking = await _bookingRepo.UpdateStatusAsync(id, statusDto.Status);
             
            return true;
        }

    }
}
