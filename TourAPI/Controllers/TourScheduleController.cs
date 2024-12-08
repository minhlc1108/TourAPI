using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Interfaces.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/tour-schedule")]
    public class TourScheduleController : ControllerBase
    {
        private readonly ITourScheduleService _tourScheduleService;

        public TourScheduleController(ITourScheduleService tourScheduleService)
        {
            _tourScheduleService = tourScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tourScheduleResults = await _tourScheduleService.GetAllAsync();
            return Ok(tourScheduleResults);
        }
    }
}