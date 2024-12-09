using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.BookingDetail;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class BookingDetailMapper
    {
        public static BookingDetail ToBookingDetailFromCreateBookingDetailReqDto(this CreateBookingDetailReqDto createBookingDetailReqDto, int customerId)
        {
            return new BookingDetail
            {
                CustomerId = customerId,
                Price = createBookingDetailReqDto.Price,
                Status = 1
            };
        }

        public static Customer ToCustomerFromCreateBookingDetailReqDto(this CreateBookingDetailReqDto createBookingDetailReqDto)
        {
            return new Customer
            {
                Name = createBookingDetailReqDto.Name,
                Birthday = createBookingDetailReqDto.Birthday,
                Sex = createBookingDetailReqDto.Sex
            };
        }

        public static BookingDetailDto ToBookingDetailDtoFromBookingDetail(this BookingDetail bookingDetail)
        {
            return new BookingDetailDto
            {
                Id = bookingDetail.CustomerId,
                Name = bookingDetail.Customer.Name,
                Price = bookingDetail.Price,
                Birthday = bookingDetail.Customer.Birthday,
            };
        }
    }
}     