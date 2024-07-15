using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Contract.IService;
using Infrastructure.Implementation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductVariationController : Controller
    {
        private readonly IProductVariationValueService _productVariationValueService;

        public ProductVariationController(IProductVariationValueService productVariationValueService)
        {
            _productVariationValueService = productVariationValueService;
        }

       // [Authorize]
        [HttpPost("Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveVariationValue([FromBody] List<ProductVariationValues> pvalueViewModel)
        {
            try
            {
                var result = await _productVariationValueService.Save(pvalueViewModel);
                if (result == 0)
                {
                    return Ok("Product Variation Value Details Save Successfully");
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

        //[Authorize]
        [HttpGet("GetProductVariation")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductVariationValues))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductVariations(string productId)
        {
            try
            {
                var result = await _productVariationValueService.GetProductVariations(productId);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("No Data Found");
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
