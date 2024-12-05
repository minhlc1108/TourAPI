using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.TourSchedule;
using TourAPI.Helpers;
using TourAPI.Interfaces.Service;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/tour-schedule")]
    public class TourScheduleController : ControllerBase
    {
        private readonly ITourScheduleService tourScheduleService;

        public TourScheduleController(ITourScheduleService tourScheduleService)
        {
            this.tourScheduleService = tourScheduleService;
        }

        [HttpGet]
        [Route("{tourId:int}")]
        public async Task<IActionResult> GetByTourId([FromRoute] int tourId, [FromQuery] TourScheduleQueryObject queryObject)
        {
            var result = await tourScheduleService.GetByTourId(tourId, queryObject);
            return Ok(result);
        }

        [HttpPost] 
        public async Task<IActionResult> Create([FromBody] CreateTourScheduleRequestDto requestDto)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var result = await tourScheduleService.CreateAsync(requestDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTourScheduleReqDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            var result = await tourScheduleService.UpdateAsync(id, requestDto);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await tourScheduleService.DeleteAsync(id);
            return Ok(result);
        }
    }
}