using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;
using TourAPI.Exceptions;
using TourAPI.Mappers;

namespace TourAPI.Service
{
    public class BookingDetailService : IBookingDetailService
    {
        private readonly IBookingDetailRepository _bookingDetailRepo;

        public BookingDetailService(IBookingDetailRepository bookingDetailRepo)
        {
            _bookingDetailRepo = bookingDetailRepo;
        }

        public async Task<BookingDetailDto?> CreateAsync(CreateBookingDetailReqDto bookingDetailDto)
        {
            var bookingDetailModel = bookingDetailDto.ToBookingDetailModel();
            var createdBookingDetail = await _bookingDetailRepo.CreateAsync(bookingDetailModel);
            return createdBookingDetail.ToBookingDetailDto();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var bookingDetail = await _bookingDetailRepo.GetByIdAsync(id);

            if (bookingDetail == null)
            {
                throw new NotFoundException("BookingDetail not found");
            }

            return await _bookingDetailRepo.DeleteByIdAsync(id);
        }

        public async Task<List<BookingDetailDto>> GetAllByBookingIdAsync(int bookingId)
        {
            var (bookingDetails, totalCount) = await _bookingDetailRepo.GetAllAsync(bookingId);
            return bookingDetails.Select(bd => bd.ToBookingDetailDto()).ToList();
        }

        public async Task<BookingDetailDto?> GetByIdAsync(int id)
        {
            var bookingDetail = await _bookingDetailRepo.GetByIdAsync(id);

            if (bookingDetail == null)
            {
                throw new NotFoundException("BookingDetail not found");
            }

            return bookingDetail.ToBookingDetailDto();
        }

        public async Task<BookingDetailDto?> UpdateAsync(int id, UpdateBookingDetailReqDto bookingDetailDto)
        {
            var bookingDetail = await _bookingDetailRepo.GetByIdAsync(id);

            if (bookingDetail == null)
            {
                throw new NotFoundException("BookingDetail not found");
            }

            bookingDetail.UpdateBookingDetailFromDto(bookingDetailDto);
            var updatedBookingDetail = await _bookingDetailRepo.UpdateAsync(bookingDetail);
            return updatedBookingDetail.ToBookingDetailDto();
        }
    }
}
