using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Tour;
using TourAPI.Helpers;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/tour")]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;

        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TourQueryObject query)
        {
            return Ok(await _tourService.GetAllAsync(query));
        }

        [HttpGet]
        [Route("get-tours")]
        public async Task<IActionResult> GetAllToursAndToursSchedule()
        {
            return Ok(await _tourService.GetAllToursAndToursSchedule());
        }

        [HttpGet]
        [Route("get-detail/{id:int}")]
        public async Task<IActionResult> GetTourDetail([FromRoute] int id)
        {
            return Ok(await _tourService.GetTourDetail(id));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tourDto = await _tourService.GetByIdAsync(id);
            return Ok(tourDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourReqDto tourDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTourDto = await _tourService.CreateAsync(tourDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTourDto.Id }, createdTourDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTourReqDto tourDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTourDto = await _tourService.UpdateAsync(id, tourDto);
            return Ok(updatedTourDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deletedTourDto = await _tourService.DeleteAsync(id);
            return Ok(deletedTourDto);
        }
    }
}