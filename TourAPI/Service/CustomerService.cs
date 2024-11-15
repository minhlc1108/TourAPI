using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;
using TourAPI.Exceptions;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;
        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }
        public async Task<PersonalUserResponseDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);

            if (customer == null)
            {
                throw new NotFoundException("Không tìm thấy danh mục");
            }

            return customer.ToPersonalUserResponseDto();
        }
    }
}