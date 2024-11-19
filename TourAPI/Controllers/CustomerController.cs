using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Account;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        // private readonly ICustomerRepository _customRepo;
        private readonly ICustomerService _customService;

         public CustomerController(ICustomerService customService)
        {
            _customService = customService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CustomerQueryObject query)
        {
             if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categorieResultDto = await _customService.GetAllAsync(query);
             Console.WriteLine("al>>>>>>",categorieResultDto);
            return Ok(categorieResultDto);
        }


        [HttpGet("{id}")]
         // Chỉ định rõ HTTP GET và route với tham số id
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var custom = await _customService.GetByIdAsync(id);
            if (custom == null)
            {
                return NotFound("Không tìm thấy Custom");
            }
            
            return Ok(custom);
        }
       

        
    }
}