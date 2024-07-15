using ApplicationCore.Models;
using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Infrastructure.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatagoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly IDistributedCache _cache;
        public CatagoryController(IProductCategoryService productCategoryService, IDistributedCache cache)
        {
            _productCategoryService = productCategoryService;
            _cache = cache;
        }

        [Authorize]
        [HttpPost("SaveCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(ProductCategoryViewModel productCategoryViewModel)
        {
            try
            {
                var result = await _productCategoryService.CreateProductCategory(productCategoryViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }

        }

        [Authorize]
        [HttpGet("GetAllCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductCategoryViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                IEnumerable<ProductCategoryViewModel> data;
                string recordKey = $"ProductCategory_{DateTime.Now:yyyyMMdd}";
                data = await _cache.GetRecordAsync<IEnumerable<ProductCategoryViewModel>>(recordKey);
                if (data == null)
                {
                    var result = await _productCategoryService.GetAllCategory();
                    if (result.Count() != 0)
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
        [HttpGet("GetCategoryById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCategoryById(string Id)
        {
            try
            {
                var result = await _productCategoryService.GetProductCategoryById(Id);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Record Found");
                }
                
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }

        }

        [Authorize]
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            try
            {
                var result = await _productCategoryService.RemoveProductCategory(categoryId);

                if (result == 0)
                {
                    return Ok("Category Details Deleted Successfully");
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
        [HttpGet("GetCategoryByParentCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCategoryByParentCategoryId(string Id)
        {
            try
            {
                var result = await _productCategoryService.GetProductCategoryByParentId(Id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Record Found");
                }

            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }

        }
    }
}
