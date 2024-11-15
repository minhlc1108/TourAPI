using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Account;
using TourAPI.Interfaces.Repository;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customRepo;

         public CustomerController(ICustomerRepository customRepo)
        {
            _customRepo = customRepo;
        }
        // private readonly ICategoryRepository _categoryRepo;

        public async Task<IActionResult> GetById([FromRoute] int id) {
            var custom =  await _customRepo.GetByIdAsync(id);
            if(custom == null) {
                return NotFound("Không tìm thấy tour");
            }
            PersonalUserResponseDto user = new PersonalUserResponseDto();
            
            return Ok(custom.ToPersonalUserResponseDto());
        }
    }
}