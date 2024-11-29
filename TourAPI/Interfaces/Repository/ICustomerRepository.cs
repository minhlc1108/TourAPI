using System.Collections.Generic;
using System.Threading.Tasks;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByAccountIdAsync(string accountId);
        Task AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string id);
    }
}
