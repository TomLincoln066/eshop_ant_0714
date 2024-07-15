using ApplicationCore.Models;
using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatagoryVariationController : ControllerBase
    {
        private readonly ICategoryVariationService _categoryVariationService;
        private readonly IDistributedCache _cache;

        public CatagoryVariationController(ICategoryVariationService categoryVariationService, IDistributedCache distributedCache)
        {
            _categoryVariationService = categoryVariationService;
            _cache = distributedCache;
        }

        [Authorize]
        [HttpPost("Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveCategoryVariation([FromBody] List<CategoryVariationViewModel> productViewModel)
        {
            try
            {
                var result = await _categoryVariationService.CreateCategory(productViewModel);
                
                if (result == 0)
                {
                    return Ok("Category variation Details Save Successfully");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }


        [Authorize]
        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategoryVariation()
        {
            try
            {
                IEnumerable<CategoryVariationViewModel> data;
                string recordKey = $"CatalogVariation_{DateTime.Now:yyyyMMdd}";
                data = await _cache.GetRecordAsync<IEnumerable<CategoryVariationViewModel>>(recordKey);
                if (data == null)
                {
                    var result = await _categoryVariationService.GetAllCategoryVariation();
                    if(result.Count() != 0)
                    {
                        await _cache.SetRecordAsync(recordKey, result);
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound("No Record Found");
                    }
                }
                else
                {
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpGet("GetCategoryVariationById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCategoryVariationById(string Id)
        {
            try
            {
                var result = await _categoryVariationService.GetCategoryById(Id);

                if (result == null)
                {
                    return NotFound("Category variation Not found");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }


       // [Authorize]
        //[HttpGet("GetCategoryVariationByCategoryId")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> GetCategoryVariationByCategoryId(string categoryId)
        //{
        //    try
        //    {
        //        var result = await _categoryVariationService.GetCategoriesByCategoryId(categoryId);

        //        if (result.Count() == 0)
        //        {
        //            return NotFound("Category variation Not found");
        //        }
        //        else
        //        {
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResponse errorResponse = new(ex);
        //        return BadRequest(errorResponse);
        //    }
        //}

      //  [Authorize]
        //[HttpDelete("Delete")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> DeleteCategoryVariation(string categoryVariationId)
        //{
        //    try
        //    {
        //        var result = await _categoryVariationService.RemoveCategory(categoryVariationId);

        //        if (result == 0)
        //        {
        //            return Ok("Category variation Details Deleted Successfully");
        //        }
        //        else
        //        {
        //            return Ok(result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResponse errorResponse = new(ex);
        //        return BadRequest(errorResponse);
        //    }
        //}
    }
}
