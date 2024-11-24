using Microsoft.AspNetCore.Mvc;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;

namespace TourAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApplicationDBContext _context;

        public CustomerService(ICustomerRepository customerRepository, ApplicationDBContext context)
        {
            _customerRepository = customerRepository;
            _context = context;
        }

        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            var result = customers.Select(customer => new
            {
                STT = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                Birthday = customer.Birthday.ToString("yyyy-MM-dd"),
                Sex = customer.Sex == 0 ? "Nam" : "Nữ",
                Status = customer.Status == 1 ? "Active" : "Locked",
                CustomerPhone = _context.Users
                                        .Where(account => account.Id == customer.AccountId)
                                        .Select(account => account.PhoneNumber)
                                        .FirstOrDefault()
            }).ToList();

            return new OkObjectResult(result);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
