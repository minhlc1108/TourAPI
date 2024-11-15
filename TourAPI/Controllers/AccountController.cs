using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourAPI.Dtos.Account;
using TourAPI.Interfaces.Service;
using TourAPI.Models;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<Account> _signInManager;

        public AccountController(UserManager<Account> userManager, ITokenService tokenService, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username.ToLower());
            if (user == null)
            {
                return Unauthorized("Tài khoản không tồn tại!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if(!result.Succeeded)
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác!");

            return Ok(new AccountResponseDto{
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var account = new Account
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.Phone
                };

                var customer = new Customer
                {
                    Name = registerDto.CustomerName,
                    Sex = registerDto.CustomerSex,
                    Address = registerDto.CustomerAddress,
                    Status = 1
                };

                var createdAccount = await _userManager.CreateAsync(account, registerDto.Password);
                if (createdAccount.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(account, "User");
                    //   add khách hàng

                    if (roleResult.Succeeded)
                    {
                        return Ok(new AccountResponseDto
                        {
                            UserName = account.UserName,
                            Email = account.Email,
                            Token = _tokenService.CreateToken(account)
                        });
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdAccount.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}