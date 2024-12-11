using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Customer;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ICustomerService
    {
        //  Task<PersonalUserResponseDto?> GetByIdAsync(int id);
        Task<CustomerDto?> GetByIdAsync(int id);
        // Task<CustomerDto?>  GetByEmailAsync(string email);
        Task<CustomerDto?> GetByEmailAsync(string email);
        Task<CustomerResultDto> GetAllAsync(CustomerQueryObject query);
        Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerReqDto CustomerDto);
        Task<CustomerDto?> GetCustomerByAccountIdAsync(string accountId);
        Task<bool> UpdateCustomerAsync(string accountId, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(string id);
        Task<IActionResult> GetCustomers();
    }
}
