using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<IActionResult> GetCustomers();
        Task<Customer?> GetCustomerByAccountIdAsync(string accountId);
        Task<bool> UpdateCustomerAsync(string accountId, UpdateCustomerDto updateCustomerDto);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
