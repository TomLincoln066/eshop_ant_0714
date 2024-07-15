using ApplicationCore.ViewModel;
using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Infrastructure.Implementation.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
       private readonly IShoppingCartService _shoppingCartService;
       public ShoppingCartController(IShoppingCartService shoppingCartService) 
        {
            _shoppingCartService = shoppingCartService;
        }

        [Authorize]
        [HttpGet("GetShoppingCartByCustomerId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetListShoppingCart(Guid customerId)
        {
            try
            {
                var result = await _shoppingCartService.GetShoppingCartByCustomerId(customerId);
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

        //[HttpGet("GetShoppingCart")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartViewModel))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> GetShoppingCart(int Id, Guid customerId)
        //{
        //    try
        //    {
        //        var result = await _shoppingCartService.GetShoppingCartByCustomerId(Id, customerId);
        //        if (result != null)
        //        {
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return NotFound("No Record Found");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResponse errorResponse = new(ex);
        //        return BadRequest(errorResponse);
        //    }
        //}

        [Authorize]
        [HttpPost("SaveShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(ShoppingCartViewModel shoppingCartViewModel)
        {
            try
            {
                var result = await _shoppingCartService.SaveShoppingCart(shoppingCartViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }

        }

        [Authorize]
        [HttpDelete("DeleteShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int Id, Guid customerId)
        {
            try
            {
                var result = await _shoppingCartService.DeleteShoppingCart(Id, customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }

        }
    }
}
