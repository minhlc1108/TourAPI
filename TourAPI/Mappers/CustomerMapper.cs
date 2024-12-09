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
                Email = customer.Account.Email,
                PhoneNumber = customer.Account.PhoneNumber,
                Password = customer.Account.PasswordHash,
                Bookings = customer.Bookings.Select(t => t.ToBookingDTO()).ToList(),
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