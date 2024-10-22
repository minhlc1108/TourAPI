using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Data;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public CategoryController(ApplicationDBContext context)
        {   
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {
            var categories  = _context.Categories.ToList();
            return Ok(categories);
        }
    }
}