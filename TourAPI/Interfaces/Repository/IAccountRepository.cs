using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityUser?> GetAccountByIdAsync(string id);
        Task<Account?> GetAccountByUsernameAsync(string username);
        Task<bool> IsUserAdminAsync(string id);
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> UpdateCustomerStatusAsync(string accountId, int status);
        Task<bool> CreateAccountAsync(Account account, string password);
        Task<bool> AddCustomerAsync(Customer customer);
        Task<IEnumerable<dynamic>> GetAllAccountsAsync(); 
        Task<bool> AccountExistsAsync(string username, string email, string phone);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<bool> SetAccountLockoutAsync(string accountId, bool lockoutEnabled); 
    }
}
