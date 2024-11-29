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
                Status = customer.Status,
            };
        }
    }
}