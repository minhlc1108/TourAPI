using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Account;
using TourAPI.Interfaces.Service;

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

    [HttpPut("updateStatus/{id}")]
    public async Task<IActionResult> UpdateStatus(string id, [FromBody] bool status)
    {
        return await _accountService.UpdateStatus(id, status);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        return await _accountService.ForgotPassword(email);
    }
}
