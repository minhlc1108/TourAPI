using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            // Console.WriteLine("al>>>>>>",categorieResultDto);
            if (customer == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }
            //    customer.Bookings.ForEach(t => 
            //    {
            //     if( t.TourSchedule != null)
            //      {Console.WriteLine("Hello, World!" , t.TourSchedule);
            //      }
            //      else 
            //      {Console.WriteLine("khong on roi ~" );

            //      }


            //    });

            //  Console.WriteLine("Hello, World!" , customer);
            // return customer.ToPersonalUserResponseDto();
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
                throw new NotFoundException("Không tìm thấy danh mục");
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
        public async Task<CustomerDto?> Create(CreateCustomerReqDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer data cannot be null.");
            }

            var customerModel = new Customer
            {
                Name = customerDto.Name,
                Address = customerDto.Address,
                Birthday = customerDto.Birthday,
                PhoneNumber = customerDto.PhoneNumber,
                Email = customerDto.Email,
                Sex = customerDto.Sex,
            };

            var createdCustomer = await _customerRepo.Create(customerModel);

            if (createdCustomer == null)
            {
                throw new Exception("Failed to create the customer.");
            }

            return createdCustomer.ToCreateCustomerDto();
        }
        public async Task<CustomerDto?> Update(UpdateCustomerReqDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto), "Customer data cannot be null.");
            }

            var existingCustomer = await _customerRepo.GetByIdAsync(customerDto.Id);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customerDto.Id} not found.");
            }

            try
            {
                existingCustomer.Name = customerDto.Name;
                existingCustomer.Address = customerDto.Address;
                existingCustomer.Birthday = customerDto.Birthday;
                existingCustomer.PhoneNumber = customerDto.PhoneNumber;
                existingCustomer.Email = customerDto.Email;
                existingCustomer.Sex = customerDto.Sex;

                var updatedCustomer = await _customerRepo.UpdateAsync(existingCustomer);
                if (updatedCustomer == null)
                {
                    throw new Exception("Failed to update the customer.");
                }

                return updatedCustomer.ToCreateCustomerDto();
            }
            catch (Exception ex)
            {
                throw;  // Re-throw to propagate the error
            }
        }


        public async Task<bool> Delete(int customerId)
        {
            var existingCustomer = await _customerRepo.GetByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return false;
            }

            _customerRepo.Delete(existingCustomer);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}