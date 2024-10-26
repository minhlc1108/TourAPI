using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Data;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;
using TourAPI.Interfaces;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
             if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var categories = await _categoryRepo.GetAllAsync(query);
            var categoriesDto = categories.Select(c => c.ToCategoryDTO()).ToList();
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
             if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var category = await _categoryRepo.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.ToCategoryDTO());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryReqDto categoryDto)
        {
             if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var categoryModel = categoryDto.ToCategoryFromCreateDTO();
            await _categoryRepo.CreateAsync(categoryModel);
            return CreatedAtAction(nameof(GetById), new { id = categoryModel.Id }, categoryModel.ToCategoryDTO());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryReqDto categoryDto)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var categoryModel = await _categoryRepo.UpdateAsync(id, categoryDto);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDTO());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            var categoryModel = await _categoryRepo.DeleteById(id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel.ToCategoryDTO());
        }
    }
}