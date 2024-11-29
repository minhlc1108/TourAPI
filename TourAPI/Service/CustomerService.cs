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
                AccountId = customer.AccountId,
                CustomerPhone = _context.Users
                                        .Where(account => account.Id == customer.AccountId)
                                        .Select(account => account.PhoneNumber)
                                        .FirstOrDefault()
            }).ToList();

            return new OkObjectResult(result);
        }

        public async Task<Customer?> GetCustomerByAccountIdAsync(string accountId)
        {
            return await _customerRepository.GetCustomerByAccountIdAsync(accountId);
        }

        public async Task<bool> UpdateCustomerAsync(string accountId, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepository.GetCustomerByAccountIdAsync(accountId);
            Console.WriteLine(accountId + " " + updateCustomerDto.Name + " " + updateCustomerDto.Sex + " " + updateCustomerDto.Address + " " + updateCustomerDto.Birthday);
            if (customer == null) return false;

            customer.Name = updateCustomerDto.Name;
            customer.Sex = updateCustomerDto.Sex;
            customer.Address = updateCustomerDto.Address;
            customer.Birthday = updateCustomerDto.Birthday;

            return await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            return await _customerRepository.DeleteCustomerAsync(id);
        }
    }
}
