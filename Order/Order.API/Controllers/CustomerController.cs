using ApplicationCore.ViewModel;
using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        [Authorize]
        [HttpGet("GetCustomerAddressByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetCustomerAddressByUserId(Guid customerId)
        {
            try
            {
                var result = await _customerService.GetCustomerById(customerId);
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

        [Authorize]
        [HttpPost("SaveCutomerAddress")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(CustomerViewModel customerViewModel)
        {
            try
            {
                var result = await _customerService.SaveCustomer(customerViewModel);
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
