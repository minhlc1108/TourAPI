using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourAPI.Helpers;
using TourAPI.Models;
using TourAPI.Dtos.Promotion;
using TourAPI.Interfaces.Service;
using TourAPI.Data;

namespace TourAPI.Controllers
{
    [ApiController]
    [Route("api/promotion")]
    public class PromotionController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PromotionQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotionsResultDto = await _promotionService.GetAllAsync(query);
            return Ok(promotionsResultDto);
        }

        [HttpGet]
        [Route("code/{code}")]
        public async Task<IActionResult> GetByCode([FromRoute] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotionsResultDto = await _promotionService.GetByCodeAsync(code);
            return Ok(promotionsResultDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotionDto = await _promotionService.GetByIdAsync(id);

            return Ok(promotionDto);
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePromotionReqDto promotionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _promotionService.IsCodeExistsAsync(promotionDto.Code))
            // if ( _promotionService.GetByCodeAsync(code)   )

            {
                ModelState.AddModelError("Code", "Mã code này đã tồn tại.");
                return BadRequest(ModelState); 
            }
            var createdPromotionDto = await _promotionService.CreateAsync(promotionDto);
            return CreatedAtAction(nameof(GetById), new { id = createdPromotionDto.Id }, createdPromotionDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePromotionReqDto promotionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedPromotionDto = await _promotionService.UpdateAsync(id, promotionDto);

            return Ok(updatedPromotionDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var promotionDto = await _promotionService.DeleteById(id);

            return Ok(promotionDto);
        }
    }
}
