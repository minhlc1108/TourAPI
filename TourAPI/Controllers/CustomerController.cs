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
}
