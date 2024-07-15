using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Promotions.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }
        
        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> GetAllPromotions()
        {
            var promotions = await _promotionService.GetAllPromotions(); 
            if (!promotions.Any())
            {
                return NotFound(new { error = "No promotions found, please try later"});
            }
            return Ok(promotions);
        }
        
        [HttpGet]
        [Route("{id:int}", Name = "GetPromotionDetails")]
        [Authorize]
        public async Task<IActionResult> GetPromotionDetails(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id); 
            if (promotion == null)
            {
                return NotFound(new { errorMessage = "No promotion found for this id" });
            }

            return Ok(promotion);
        }
        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePromotion(PromotionRequestModel model)
        {
            if (!ModelState.IsValid)
                // 400 status code
                return BadRequest();

            var promotion = await _promotionService.AddPromotionAsync(model);
            return CreatedAtAction
                ("GetPromotionDetails", new { controller = "Promotion", id = promotion }, "Promotion Created");
        }
        
        [HttpDelete]
        [Route("delete-{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound(new { errorMessage = "No promotion found for this id" });
            }
            var response = new { Message = "Promotion is deleted" };
            if (await _promotionService.DeletePromotionAsync(id) > 0)
                return Ok(response);
            return NoContent();
        }
        
        [HttpPut]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(PromotionRequestModel model)
        {
            var response = new { Message = "Promotion is updated" };
            if (await _promotionService.UpdatePromotionAsync(model) > 0)
                return Ok(response);
            return NoContent();
        }
        
        [HttpGet]
        [Route("promotionByProductName")]
        [Authorize]
        public async Task<IActionResult> GetpromotionByProductCategory(string productName)
        {
            var promotions = await _promotionService.GetPromotionByProduct(productName);
            if (!promotions.Any())
            {
                return NotFound(new { errorMessage = "No promotion found for this product" });
            }
            return Ok(promotions);
        }
        
        [HttpGet]
        [Route("activePromotions")]
        [Authorize]
        public async Task<IActionResult> GetActivePromotions()
        {
            var promotions = await _promotionService.GetAllActivePromotion();
            if (!promotions.Any())
            {
                return NotFound(new { errorMessage = "No Active promotions" });
            }
            return Ok(promotions);
        }
    }
}
