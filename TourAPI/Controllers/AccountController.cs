using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Account;
using TourAPI.Interfaces.Service;
using TourAPI.Service;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("listAccount")]
    public async Task<IActionResult> GetAccounts()
    {
        return await _accountService.GetAccounts();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);  

        return await _accountService.Login(loginDto);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); 

        return await _accountService.Register(registerDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(string id)
    {
        var account = await _accountService.GetAccountById(id);
        if (account == null)
        {
            return NotFound(new { message = "Tài khoản không tồn tại!" });
        }

        return Ok(account);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAccount(string id, [FromBody] UpdateAccountDto updateAccountDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return await _accountService.UpdateAccount(id, updateAccountDto);
    }

    [HttpPut("updateStatus/{id}")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] bool status)
    {
        return await _accountService.UpdateStatus(id, status);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAccount(string id)
    {
        return await _accountService.DeleteAccountAsync(id);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        return await _accountService.ForgotPassword(email);
    }
}
