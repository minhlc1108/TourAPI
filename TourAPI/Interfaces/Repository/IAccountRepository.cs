using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TourAPI.Models;

namespace TourAPI.Interfaces.Repository
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync(string id);
        Task<Account?> GetAccountByUsernameAsync(string username);
        Task<Account> GetAccountByPhoneAsync(string phone);
        Task<bool> IsUserAdminAsync(string id);
        Task<bool> UpdateAccountAsync(Account account);
        Task<bool> UpdateCustomerStatusAsync(string accountId, int status);
        Task<bool> UpdateUserRolesAsync(Account account, string newRole);
        Task<bool> CreateAccountAsync(Account account, string password);
        Task<bool> DeleteAccountAsync(string id);
        Task<IEnumerable<dynamic>> GetAllAccountsAsync(); 
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<bool> SetAccountLockoutAsync(string accountId, bool lockoutEnabled); 
    }
}
