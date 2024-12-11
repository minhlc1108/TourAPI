using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TourAPI.Dtos.Account;
using TourAPI.Exceptions;
using TourAPI.Interfaces;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Models;
using TourAPI.Repository;

namespace TourAPI.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly SignInManager<Account> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IEmailSender _emailSender;

        public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository, ITokenService tokenService, IEmailSender emailSender, SignInManager<Account> signInManager)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountRepository.GetAllAccountsAsync();
            return new OkObjectResult(accounts);
        }

        public async Task<AccountDto> GetAccountById(string id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                return null;
            }

            return new AccountDto
            {
                UserName = account.UserName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                LockoutEnabled = account.LockoutEnabled,
                Role = await _accountRepository.IsUserAdminAsync(id) ? 1 : 0,
            };
        }


        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _accountRepository.GetAccountByUsernameAsync(loginDto.Username);
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
                Token =  await _tokenService.CreateToken(user),
                IsAdmin = isAdmin
            });
        }

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var existingAccountByUsername = await _accountRepository.GetAccountByUsernameAsync(registerDto.Username);
                if (existingAccountByUsername != null)
                {
                    return new BadRequestObjectResult(new { message = "Tên đăng nhập đã tồn tại." });
                }

                var existingAccountByEmail = await _accountRepository.GetAccountByEmailAsync(registerDto.Email);
                if (existingAccountByEmail != null)
                {
                    return new BadRequestObjectResult(new { message = "Email đã được sử dụng." });
                }

                var account = new Account
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone,
                };

                var createdAccount = await _accountRepository.CreateAccountAsync(account, registerDto.Password);
                if (createdAccount)
                {
                    // Tạo khách hàng liên kết với tài khoản
                    var customer = new Customer
                    {
                        Name = registerDto.CustomerName,
                        Sex = registerDto.CustomerSex,
                        Address = registerDto.CustomerAddress,
                        Birthday = registerDto.Birthday,
                        Status = 1,
                        AccountId = account.Id
                    };
                    await _customerRepository.AddCustomerAsync(customer);

                    // Trả về kết quả
                    return new OkObjectResult(new NewAccountDto
                    {
                        UserName = account.UserName,
                        Email = account.Email,
                        // Token =  await _tokenService.CreateToken(account)
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

        public async Task<IActionResult> UpdateAccount(string id, UpdateAccountDto updateAccountDto)
        {
            Console.WriteLine("Nhận yêu cầu cập nhật tài khoản:", JsonConvert.SerializeObject(updateAccountDto));
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account == null)
            {
                Console.WriteLine("Không tìm thấy tài khoản với ID:", id);
                return new NotFoundObjectResult(new { message = "Không tìm thấy tài khoản!" });
            }

            var existingAccountByEmail = await _accountRepository.GetAccountByEmailAsync(updateAccountDto.Email);
            if (existingAccountByEmail != null && existingAccountByEmail.Id != id)
            {
                return new BadRequestObjectResult(new { message = "Email đã được sử dụng." });
            }


            account.UserName = updateAccountDto.UserName;
            account.Email = updateAccountDto.Email;
            account.PhoneNumber = updateAccountDto.PhoneNumber;
            if (!string.IsNullOrEmpty(updateAccountDto.Password))
            {
                account.PasswordHash = HashPasswordUsingIdentity(updateAccountDto.Password);
            }
            var result = await _accountRepository.UpdateAccountAsync(account);
            if (!result)
            {
                return new BadRequestObjectResult(new { message = "Cập nhật tài khoản và mật khẩu thất bại!" });
            }

            var updateRoleResult = await _accountRepository.UpdateUserRolesAsync(account, updateAccountDto.Role);
            if (!updateRoleResult)
            {
                return new BadRequestObjectResult(new { message = "Cập nhật vai trò thất bại!" });
            }

            return new JsonResult(new { message = "Cập nhật tài khoản và thông tin khách hàng thành công!" });
        }

        public async Task<IActionResult> DeleteAccountAsync(string id)
        {
            try
            {
                var success = await _accountRepository.DeleteAccountAsync(id);
                if (success)
                    return new OkObjectResult(new { message = "Xóa tài khoản thành công" });
                return new NotFoundObjectResult(new { message = "Không tìm thấy tài khoản" });
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

        public async Task<bool> CheckIsAdmin(string email)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(email);
            if (account == null)
            {
               throw new NotFoundException("Tài khoản không tồn tại!");
            }
            return await _accountRepository.IsUserAdminAsync(account.Id);
        }
    }
}
