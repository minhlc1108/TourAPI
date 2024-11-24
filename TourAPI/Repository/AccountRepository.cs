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
            return result.Succeeded;
        }

        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return true;
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
                    CustomerName = _context.Customers
                        .Where(c => c.AccountId == user.Id)
                        .Select(c => c.Name)
                        .FirstOrDefault()
                })
                .ToListAsync();
        }


        public async Task<bool> AccountExistsAsync(string username, string email, string phone)
        {
            return await _context.Users
                .AnyAsync(u => u.UserName == username || u.Email == email || u.PhoneNumber == phone);
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
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

        public async Task<IdentityUser?> GetAccountByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<Account?> GetAccountByUsernameAsync(string username)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> IsUserAdminAsync(string id)
        {
            // Lấy tất cả các role của người dùng
            var userRoles = await _context.UserRoles
                .Where(ur => ur.UserId == id)
                .ToListAsync(); // Lấy tất cả các roles của người dùng

            // Tìm kiếm role Admin
            var adminRole = await _context.Roles
                .Where(r => r.Name == "Admin")
                .FirstOrDefaultAsync(); // Tìm kiếm role Admin

            if (adminRole == null)
            {
                return false; // Nếu role Admin không tồn tại, trả về false
            }

            // Kiểm tra xem người dùng có role Admin hay không
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
    }
}
