using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;

namespace TourAPI.Interfaces.Service
{
    public interface IAccountService
    {
        Task<IActionResult> GetAccounts();
        Task<AccountDto> GetAccountById(string id);
        Task<IActionResult> Login(LoginDto loginDto);
        Task<IActionResult> Register(RegisterDto registerDto);
        Task<IActionResult> UpdateAccount(string id, UpdateAccountDto updateAccountDto);
        Task<IActionResult> UpdateStatus(string id, bool status);
        Task<IActionResult> DeleteAccountAsync(string id);
        Task<IActionResult> ForgotPassword(string email);
    }
}
