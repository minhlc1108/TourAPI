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
    }
}
