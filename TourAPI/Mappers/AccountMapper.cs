using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Customer;
using TourAPI.Models;

namespace TourAPI.Mappers
{
    public static class AccountMapper
    {
        public static PersonalUserResponseDto ToPersonalUserResponseDto(this Customer customer)
        {
            return new PersonalUserResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Sex = customer.Sex,
                // Email = customer.Account?.Email ?? "Unknown", // Gán giá trị mặc định nếu Account null
                // PhoneNumber = customer.Account?.PhoneNumber ?? "Unknown",
                Email = customer.Account.Email,
                PhoneNumber = customer.Account.PhoneNumber,
                Birthday = customer.Birthday,
                Address = customer.Address,
                Password = customer.Account.PasswordHash,
                // Bookings = customer.Bookings.Select(t => t.ToBookingDTO()).ToList(),
       

            };
        }



    }
}