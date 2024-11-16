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
        private readonly ICategoryService _categoryService;

        public TourController(ITourService tourService, ICategoryService categoryService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TourQueryObject query)
        {
            return Ok(await _tourService.GetAllAsync(query));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var tourDto =  await _tourService.GetByIdAsync(id);
            return Ok(tourDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourReqDto tourDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _categoryService.GetByIdAsync(tourDto.CategoryId);
            var createdTourDto = await _tourService.CreateAsync(tourDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTourDto.Id }, createdTourDto);
        }

    }
}