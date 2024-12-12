using TourAPI.Dtos.Bookings;
using TourAPI.Dtos.Customer;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Sex = customer.Sex,
                Address = customer.Address,
                Birthday = customer.Birthday,
                Email = customer.Account?.Email ?? "Unknown",  // Kiểm tra null trước khi truy cập
                PhoneNumber = customer.Account?.PhoneNumber?? "Unknown",  // Kiểm tra null trước khi truy cập
                Password = customer.Account?.PasswordHash ?? "Unknown",  // Kiểm tra null trước khi truy cập
                Bookings = customer.Bookings?.Select(t => t.ToBookingDTO()).ToList() ?? new List<BookingDto>(), // Kiểm tra null trước khi truy cập
                Status = customer.Status,
            };
        }
        public static OrdererDto ToOrdererDto(this Customer customer)
        {
            return new OrdererDto
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.Phone,
                Address = customer.Address
            };
        }
    }

}


// Email = customer.Account?.Email ?? "Unknown", // Gán giá trị mặc định nếu Account null
// PhoneNumber = customer.Account?.PhoneNumber ?? "Unknown",