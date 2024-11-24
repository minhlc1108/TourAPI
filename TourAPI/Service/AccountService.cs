using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Account;
using TourAPI.Interfaces;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;

namespace TourAPI.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly SignInManager<Account> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public AccountService(IAccountRepository accountRepository, ITokenService tokenService, IEmailSender emailSender, SignInManager<Account> signInManager)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return new OkObjectResult(accounts);
        }

        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _accountRepository.GetAccountByUsernameAsync(loginDto.Username);
            Console.WriteLine(user.UserName + " " + user.LockoutEnabled);
            if (user == null)
            {
                return new UnauthorizedObjectResult("Tài khoản không tồn tại!");
            }

            if (user.LockoutEnabled)
            {
                return new UnauthorizedObjectResult("Tài khoản đã bị khóa!");
            }


            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return new UnauthorizedObjectResult("Tài khoản hoặc mật khẩu không chính xác!");
            }

            var isAdmin = await _accountRepository.IsUserAdminAsync(user.Id);
            return new OkObjectResult(new
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                IsAdmin = isAdmin
            });
        }

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var existingAccount = await _accountRepository.GetAccountByUsernameAsync(registerDto.Username);
                if (existingAccount != null)
                {
                    return new BadRequestObjectResult(new { message = "Tên đăng nhập đã tồn tại." });
                }

                var existingEmail = await _accountRepository.GetAccountByUsernameAsync(registerDto.Email);
                if (existingEmail != null)
                {
                    return new BadRequestObjectResult(new { message = "Email đã được sử dụng." });
                }

                var existingPhone = await _accountRepository.GetAccountByUsernameAsync(registerDto.Phone);
                if (existingPhone != null)
                {
                    return new BadRequestObjectResult(new { message = "Số điện thoại đã được sử dụng." });
                }

                var account = new Account
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone,
                    LockoutEnabled = false,
                };

                var createdAccount = await _accountRepository.CreateAccountAsync(account, registerDto.Password);
                if (createdAccount)
                {
                    var customer = new Customer
                    {
                        Name = registerDto.CustomerName,
                        Sex = registerDto.CustomerSex,
                        Address = registerDto.CustomerAddress,
                        Birthday = registerDto.Birthday,
                        Status = 1,
                        AccountId = account.Id
                    };
                    await _accountRepository.AddCustomerAsync(customer);

                    return new OkObjectResult(new NewAccountDto
                    {
                        UserName = account.UserName,
                        Email = account.Email,
                        Token = _tokenService.CreateToken(account)
                    });
                }
                else
                {
                    return new StatusCodeResult(500);
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> UpdateStatus(string id, bool status)
        {
            try
            {
                int statusToInt = status ? 0 : 1;
                var success = await _accountRepository.UpdateCustomerStatusAsync(id, statusToInt);
                var check = await _accountRepository.SetAccountLockoutAsync(id, status);
                //Console.WriteLine(success + " " + check + " " + status + " " + statusToInt);
                if (success && check)
                {
                    return new OkObjectResult(new { message = "Status updated successfully" });
                }
                else
                {
                    return new NotFoundObjectResult(new { message = "Account not found" });
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500); // Lỗi chung
            }
        }

        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var user = await _accountRepository.GetAccountByEmailAsync(email);
                if (user == null)
                {
                    return new BadRequestObjectResult("Email không tồn tại: " + email);
                }

                var newPassword = GenerateRandomPassword();

                user.PasswordHash = HashPasswordUsingIdentity(newPassword);
                var result = await _accountRepository.UpdateAccountAsync(user);

                if (result)
                {
                    var subject = "Mật khẩu mới của bạn";
                    var message = $"Mật khẩu mới của bạn là: <strong>{newPassword}</strong>";
                    await _emailSender.SendEmailAsync(email, subject, message);

                    return new OkObjectResult("Mật khẩu mới đã được gửi tới email của bạn!");
                }
                else
                {
                    return new StatusCodeResult(500); 
                }
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(500); 
            }
        }
        private string HashPasswordUsingIdentity(string password)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            return passwordHasher.HashPassword(new IdentityUser(), password);
        }

        private string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{}|;:,.<>?/~";
            var random = new Random();
            var password = new string(Enumerable.Range(0, length)
                                              .Select(_ => validChars[random.Next(validChars.Length)])
                                              .ToArray());
            return password;
        }
    }
}
