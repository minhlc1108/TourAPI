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

namespace TourAPI.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepo;


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
    }
}
