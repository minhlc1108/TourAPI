using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/tour-image")]
    public class TourImageController : ControllerBase
    {
        private readonly CloudinaryService _cloudinaryService;

        public TourImageController(CloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var result = await _cloudinaryService.UploadImageAsync(file);
            return Ok(result);
        }
    }
}