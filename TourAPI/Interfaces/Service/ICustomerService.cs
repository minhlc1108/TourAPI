using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;
using TourAPI.Dtos.Customer;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ICustomerService
    {
         Task<PersonalUserResponseDto?> GetByIdAsync(int id);
        Task<CustomerResultDto> GetAllAsync(CustomerQueryObject query);


    }
}