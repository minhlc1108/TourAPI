using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Data;
using TourAPI.Dtos.Account;
using TourAPI.Dtos.Category;
using TourAPI.Dtos.Customer;
using TourAPI.Exceptions;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        private readonly ApplicationDBContext _context;
        public CustomerService(ICustomerRepository customerRepo, ApplicationDBContext context)
        {
            _customerRepo = customerRepo;
            _context = context;
        }

        // public async Task<PersonalUserResponseDto?> GetByIdAsync(int id)
        // {
        //     var customer = await _customerRepo.GetByIdAsync(id);
        //     // Console.WriteLine("al>>>>>>",categorieResultDto);
        //     if (customer == null)
        //     {
        //         throw new NotFoundException("Không tìm thấy danh mục");
        //     }

        //     // return customer.ToPersonalUserResponseDto();
        //     return customer.ToPersonalUserResponseDto();

        // }


        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerRepo.GetAllCustomersAsync();
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

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            // Console.WriteLine("al>>>>>>",categorieResultDto);
            if (customer == null)
            {
                throw new NotFoundException("Không tìm thấy khách hàng");
            }
            return customer.ToCustomerDto();
        }



        public async Task<CustomerResultDto> GetAllAsync(CustomerQueryObject query)
        {


            var (customers, totalCount) = await _customerRepo.GetAllAsync(query);

            var customersDto = customers.Select(c => c.ToCustomerDto()).ToList();
            // Console.WriteLine("al>>>>>>",customersDto);
            return new CustomerResultDto
            {
                Customers = customersDto,
                Total = totalCount
            };
        }
        public async Task<CustomerDto?> UpdateAsync(int id, UpdateCustomerReqDto CustomerDto)
        {
            var customerModel = await _customerRepo.GetByIdAsync(id);
            if (customerModel == null)
            {
                throw new NotFoundException("Không tìm thấy khách hàng");
            }

            customerModel.Name = CustomerDto.Name;
            customerModel.Sex = CustomerDto.Sex;
            customerModel.Address = CustomerDto.Address;
            customerModel.Birthday = CustomerDto.Birthday;
            customerModel.Account.PhoneNumber = CustomerDto.PhoneNumber;
            customerModel.Account.PasswordHash = CustomerDto.Password;

            await _customerRepo.UpdateAsync(customerModel);

            return customerModel.ToCustomerDto();
        }

        // public async Task<Customer?> GetCustomerByAccountIdAsync(string accountId)
        // {
        //     return await _customerRepo.GetCustomerByAccountIdAsync(accountId);
        // }

        public async Task<CustomerDto?> GetCustomerByAccountIdAsync(string accountId)
        {
            var customer = await _customerRepo.GetCustomerByAccountIdAsync(accountId);
            if (customer == null)
            {
                return null;
            }

            // Map từ Customer sang CustomerDto
            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                // Thêm các thuộc tính khác tùy theo định nghĩa của CustomerDto
            };
        }


        public async Task<CustomerDto?> GetByEmailAsync(string Email)
        {
            var customer = await _customerRepo.GetCustomerByEmailAsync(Email);
            if (customer == null)
                    {
                        return null; // Hoặc throw một exception nếu cần
                    }

            return customer.ToCustomerDto();
        }
        // Service
        // public async Task<CustomerDto?> GetByEmailAsync(string email)
        // {
        //     var customer = await _customerRepo.GetCustomerByEmailAsync(email);
        //     if (customer == null)
        //     {
        //         return null; // Hoặc throw một exception nếu cần
        //     }

        //     // Chuyển từ Customer Entity sang CustomerDto
        //     var customerDto = new CustomerDto
        //     {
        //         Id = customer.Id,
        //         Name = customer.Name,
        //         Sex = customer.Sex,
        //         Address = customer.Address,
        //         Birthday = customer.Birthday,
        //         Email = customer.Account.Email,
        //         PhoneNumber = customer.Account.PhoneNumber,
        //         Password = customer.Account.PasswordHash,
        //         Bookings = customer.Bookings.Select(t => t.ToBookingDTO()).ToList(),
        //         Status = customer.Status,
        //     };

        //     return customerDto; // Trả về DTO thay vì entity
        // }


        //  public async Task<CustomerDto?> GetByEmailAsync(string Email)
        // {
        //     var customer = await _customerRepo.GetByEmailAsync(Email);

        //     if (customer == null)
        //     {
        //         throw new NotFoundException("Không tìm thấy khách hàng");
        //     }
        //     return customer.ToCustomerDto();
        // }

        public async Task<bool> UpdateCustomerAsync(string accountId, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _customerRepo.GetCustomerByAccountIdAsync(accountId);
            if (customer == null) return false;

            customer.Name = updateCustomerDto.Name;
            customer.Sex = updateCustomerDto.Sex;
            customer.Address = updateCustomerDto.Address;
            // customer.Birthday = updateCustomerDto.Birthday;

            return await _customerRepo.UpdateCustomerAsync(customer);
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            return await _customerRepo.DeleteCustomerAsync(id);
        }
    }
}