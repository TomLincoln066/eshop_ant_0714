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
    public class PaymentController : ControllerBase
    {
         private readonly IPaymentMethodService _paymentMethodService;
        public PaymentController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [Authorize]
        [HttpGet("GetPaymentByCustomerId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShoppingCartViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPaymentByCustomerId(Guid customerId)
        {
            try
            {
                var result = await _paymentMethodService.GetPaymentMethods(customerId);
                if (result.Count() >= 0)
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
        [HttpPost("SavePayment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var result = await _paymentMethodService.SavePaymentMethod(paymentMethodViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpDelete("DeletePayment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var result = await _paymentMethodService.DeletePaymentMethod(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpPut("UpdatePayment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var result = await _paymentMethodService.UpdatePaymentMethod(paymentMethodViewModel);
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
