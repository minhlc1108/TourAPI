using System.Collections.Generic;
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
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByAccountIdAsync(string accountId);
        Task AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
    }
}
