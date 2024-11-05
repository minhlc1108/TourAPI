using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Data;
using TourAPI.Dtos.Category;
using TourAPI.Helpers;
using TourAPI.Interfaces.Service;
using TourAPI.Mappers;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CategoryQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categorieResultDto = await _categoryService.GetAllAsync(query);
            return Ok(categorieResultDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryDto = await _categoryService.GetByIdAsync(id);

            return Ok(categoryDto);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryReqDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdCategoryDto = await _categoryService.CreateAsync(categoryDto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategoryDto.Id }, createdCategoryDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCategoryReqDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedCategoryDto = await _categoryService.UpdateAsync(id, categoryDto);

            return Ok(updatedCategoryDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoryDto = await _categoryService.DeleteById(id);
         
            return Ok(categoryDto);
        }
    }
}