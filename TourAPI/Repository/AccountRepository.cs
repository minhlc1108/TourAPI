using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TourAPI.Data;
using TourAPI.Models;
using TourAPI.Interfaces.Repository;

namespace TourAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<Account> _userManager;

        public AccountRepository(ApplicationDBContext context, UserManager<Account> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> CreateAccountAsync(Account account, string password)
        {
            var result = await _userManager.CreateAsync(account, password);
            if (result.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(account, "User");
                if (roleResult.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IEnumerable<dynamic>> GetAllAccountsAsync()
        {
            return await _context.Users
                .Select(user => new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.LockoutEnabled,
                    user.PhoneNumber,
                    CustomerName = _context.Customers
                        .Where(c => c.AccountId == user.Id)
                        .Select(c => c.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<Account> GetAccountByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(a => a.PhoneNumber == phone);
        }

        public async Task<bool> SetAccountLockoutAsync(string accountId, bool lockoutEnabled)
        {
            var account = await _context.Users.FindAsync(accountId);
            if (account == null)
            {
                return false;
            }

            account.LockoutEnabled = lockoutEnabled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account?> GetAccountByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Account?> GetAccountByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> IsUserAdminAsync(string id)
        {
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == id)
                .ToListAsync();

            var adminRole = await _context.Roles
                .Where(r => r.Name == "Admin")
                .FirstOrDefaultAsync();

            if (adminRole == null)
            {
                return false;
            }

            return userRoles.Any(ur => ur.RoleId == adminRole.Id);
        }


        public async Task<bool> UpdateAccountAsync(Account account)
        {
            var result = await _userManager.UpdateAsync(account);
            return result.Succeeded;
        }

        public async Task<bool> UpdateCustomerStatusAsync(string accountId, int status)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.AccountId == accountId);
            if (customer == null)
            {
                return false;
            }

            customer.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserRolesAsync(Account account, string newRole)
        {
            var currentRoles = await _userManager.GetRolesAsync(account);
            if (!currentRoles.Contains(newRole))
            {
                foreach (var role in currentRoles)
                {
                    await _userManager.RemoveFromRoleAsync(account, role);
                }
                var result = await _userManager.AddToRoleAsync(account, newRole);
                return result.Succeeded;
            }
            return true;
        }

        public async Task<bool> DeleteAccountAsync(string id)
        {
            try
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account != null)
                {
                    var roles = await _userManager.GetRolesAsync(account);
                    foreach (var role in roles)
                    {
                        await _userManager.RemoveFromRoleAsync(account, role);
                    }

                    _context.Users.Remove(account);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting account: {ex.Message}");
                throw;
            }
        }

    }
}
