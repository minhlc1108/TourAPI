using Microsoft.AspNetCore.Mvc;
using TourAPI.Interfaces.Service;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("listCustomer")]
    public async Task<IActionResult> GetCustomers()
    {
        return await _customerService.GetCustomers();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomerByAccountId(string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var customer = await _customerService.GetCustomerByAccountIdAsync(id);
        return Ok(customer);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateCustomer(string id, [FromBody] UpdateCustomerDto updateCustomerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _customerService.UpdateCustomerAsync(id, updateCustomerDto);
        return Ok("Cập nhật thông tin khách hàng thành công!");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _customerService.DeleteCustomerAsync(id);
        return Ok("Xóa khách hàng thành công!");
    }
}
