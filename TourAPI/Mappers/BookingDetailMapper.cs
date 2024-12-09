using TourAPI.Dtos.BookingDetail;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class BookingDetailMapper
    {
        public static BookingDetailDto ToBookingDetailDto(this BookingDetail bookingDetail)
        {
            if (bookingDetail == null)
            {
                return null;
            }

            return new BookingDetailDto
            {
                BookingId = bookingDetail.BookingId,
                CustomerId = bookingDetail.CustomerId,
                Price = bookingDetail.Price,
                Detail = bookingDetail.Detail,
                Status = bookingDetail.Status,
            };
        }

        public static BookingDetail ToBookingDetailModel(this CreateBookingDetailReqDto bookingDetailDto)
        {
            return new BookingDetail
            {
                Price = bookingDetailDto.Price,
                Detail = bookingDetailDto.Detail,
                Status = bookingDetailDto.Status,
                BookingId = bookingDetailDto.BookingId,
                CustomerId = bookingDetailDto.CustomerId
            };
        }

        public static void UpdateBookingDetailFromDto(this BookingDetail bookingDetail, UpdateBookingDetailReqDto bookingDetailDto)
        {
            bookingDetail.Price = bookingDetailDto.Price;
            bookingDetail.Detail = bookingDetailDto.Detail;
            bookingDetail.Status = bookingDetailDto.Status;
            bookingDetail.BookingId = bookingDetailDto.BookingId;
            bookingDetail.CustomerId = bookingDetailDto.CustomerId;
        }
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
