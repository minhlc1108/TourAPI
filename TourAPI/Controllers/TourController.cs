using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Dtos.Tour;
using TourAPI.Interfaces.Repository;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/tour")]
    public class TourController : ControllerBase
    {
        private readonly ITourRepository _tourRepo;
        private readonly ICategoryRepository _categoryRepo;

        public TourController(ITourRepository tourRepo, ICategoryRepository categoryRepo)
        {
            _tourRepo = tourRepo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll() {
            var tours = await _tourRepo.GetAllAsync();
            var toursDto = tours.Select(t => t.ToTourDTO()).ToList();
            return Ok(toursDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var tour =  await _tourRepo.GetByIdAsync(id);
            if(tour == null) {
                return NotFound("Không tìm thấy tour");
            }
            return Ok(tour.ToTourDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTourDto tourDto){
              if (!ModelState.IsValid)
                return BadRequest(ModelState);

              var category = await _categoryRepo.GetByIdAsync(tourDto.CategoryId);
              if(category == null) {
                return BadRequest("Danh mục tour không tồn tại!");
              }

              var tourModel = tourDto.ToTourFromCreateDTO();
              await _tourRepo.CreateAsync(tourModel);
              return CreatedAtAction(nameof(GetById), new {id = tourModel.Id }, tourModel.ToTourDTO());
        }
        
    }
}