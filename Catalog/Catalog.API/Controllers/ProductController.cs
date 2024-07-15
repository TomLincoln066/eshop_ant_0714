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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUriService _uriService;
        private readonly IDistributedCache _cache;

        public ProductController(IProductService productService, IUriService uriService, IDistributedCache cache)
        {
            _productService = productService;
            _uriService = uriService;
            _cache = cache;
        }

        [Authorize]
        [HttpGet("GetListProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetListProducts([FromQuery] PaginationFilter filter, string CategoryName)
        {
            try
            {
                GetAllProductswithTotalCount catalog;
                var route = Request.Path.Value;
                string recordKey = $"Catalog_{CategoryName}_{filter.PageNumber}_{filter.PageSize}";
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

                catalog = await _cache.GetRecordAsync<GetAllProductswithTotalCount>(recordKey);
                
                if(catalog is null)
                {

                    catalog = await _productService.GetAllProducts(validFilter.PageNumber, validFilter.PageSize, CategoryName);
                    if (catalog != null)
                    {
                        await _cache.SetRecordAsync(recordKey, catalog); // set cache
                        var pagedResponse = PaginationHelper.CreatePagedReponse<ProductViewModel>(catalog.products, validFilter, catalog.TotalProduct, _uriService, route);
                        return Ok(pagedResponse);
                    }
                    else
                    {
                        return NotFound("No Record Found");
                    }
                }
                else
                {
                    var pagedResponse = PaginationHelper.CreatePagedReponse<ProductViewModel>(catalog.products, validFilter, catalog.TotalProduct, _uriService, route);
                    return Ok(pagedResponse);
                }
                
                
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpGet("GetProductById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductById(string Id)
        {
            try
            {
                var result = await _productService.GetProductById(Id);
                if(result == null)
                {
                    return Ok("Product Not Found");
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
        [HttpPost("Save")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveProduct([FromBody] ProductViewModelV2 productViewModel)
        {
            try
            {
                var result = await _productService.CreateProduct(productViewModel);
                if(result != null)
                {
                    return Ok(result);
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
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModelV2 productViewModel)
        {
            try
            {
                var result = await _productService.UpdateProduct(productViewModel.Id, productViewModel);
                if (result == 0)
                {
                    return Ok("Product Details Update Successfully");
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
        [HttpPut("InActive")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InActiveProduct(string Id)
        {
            try
            {
                var result = await _productService.InActiveProduct(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }


        [Authorize]
        [HttpGet("GetProductByCategoryId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductByCategoryId(string Id)
        {
            try
            {
                var result = await _productService.GetProductByCategoryId(Id);
                if (result == null)
                {
                    return NotFound("Product Not Found");
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
        [HttpGet("GetProductByName")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            try
            {
                var result = await _productService.GetProductByName(productName.Trim());
                if (result == null)
                {
                    return NotFound("Product Not Found");
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
        [HttpDelete("DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteProduct(string ProductId)
        {
            try
            {
                var result = await _productService.DeleteProduct(ProductId);
                if (result == -1)
                {
                    return NotFound("Product Not Found");
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
    }
}
