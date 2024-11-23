using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourAPI.Helpers;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Task<(List<Customer>, int totalCount)> GetAllAsync(CustomerQueryObject query);
         Task<Customer?> GetByIdAsync(int id);
        Task<Customer?> UpdateAsync(Customer customerModel);

    }
}