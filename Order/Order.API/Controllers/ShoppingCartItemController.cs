using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;
        public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
        }

        //[HttpDelete("DeleteShoppingCartItem")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> DeleteByCartId(int CartId)
        //{
        //    try
        //    {
        //        var result = await _shoppingCartItemService.DeleteShoppingCartItem(CartId);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorResponse errorResponse = new(ex);
        //        return BadRequest(errorResponse);
        //    }

        //}

        [Authorize]
        [HttpDelete("DeleteShoppingCartItemById")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShoppingCartItemById(int Id)
        {
            try
            {
                var result = await _shoppingCartItemService.DeleteShoppingCartItemById(Id);
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
