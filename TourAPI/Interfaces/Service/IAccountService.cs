using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourAPI.Dtos.Account;

namespace TourAPI.Interfaces.Service
{
    public interface IAccountService
    {
        Task<IActionResult> GetAccounts();
        Task<IActionResult> Login(LoginDto loginDto);
        Task<IActionResult> Register(RegisterDto registerDto);
        Task<IActionResult> UpdateStatus(string id, bool status);
        Task<IActionResult> ForgotPassword(string email);
    }
}
