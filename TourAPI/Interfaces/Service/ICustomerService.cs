using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Service
{
    public interface ICustomerService
    {
        Task<IActionResult> GetCustomers();
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
    }
}
