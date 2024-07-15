using ApplicationCore.ViewModel;
using Infrastructure.Common;
using Infrastructure.Contract.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Order.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUriService _uriService;
        private readonly IRabitMQProducerConsumer _rabbitMQProducer;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDocumentService _documentService;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderService orderService, IUriService uriService, IRabitMQProducerConsumer rabbitMQProducer, IHttpContextAccessor httpContextAccessor, IDocumentService documentService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _uriService = uriService;
            _rabbitMQProducer = rabbitMQProducer;
            _httpContextAccessor = httpContextAccessor;
            _documentService = documentService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetAllOrders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllOrders([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            try
            {
                var result = await _orderService.GetAllOrders(validFilter.PageNumber, validFilter.PageSize);
                var pagedResponse = PaginationHelper.CreatePagedReponse<OrderViewModel>(result.orders, validFilter, result.TotalOrders, _uriService, route);

                if (pagedResponse == null)
                {
                    return NotFound();
                }
                else
                {
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
        [HttpPost("SaveOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SaveOrder([FromBody] OrderViewModel orderViewModel)
        {
            try
            {
                var result = await _orderService.SaveOrder(orderViewModel);
                if (result == 1)
                {
                    return Ok("Order Saved Successfully");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpGet("CheckOrderHistory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId, [FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            try
            {
                var result = await _orderService.GetAllOrdersByCustomerId(customerId, filter.PageNumber, filter.PageSize);
                var pagedResponse = PaginationHelper.CreatePagedReponse<OrderViewModel>(result.orders, validFilter, result.TotalOrders, _uriService, route);
                if (pagedResponse == null)
                {
                    return NoContent();
                }
                else
                {
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
        [HttpGet("CheckOrderStatus")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CheckOrderStatus(int orderId, Guid customerId)
        {
            try
            {

                var result = await _orderService.OrderStatus(orderId, customerId);
                if (result == null)
                {
                    return NoContent();
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
        [HttpPut("CancelOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrder(int orderId, Guid customerId)
        {
            try
            {
                var result = await _orderService.CancelOrder(orderId, customerId);
                if (result == 0)
                {
                    return NoContent();
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
        [HttpPost("OrderCompleted")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> OrderCompleted(decimal bill, Guid customerId, string customerEmail)
        {
            try
            {
                //string email = null;
                //ClaimsPrincipal user = User;
                //Claim emailClaim = user.FindFirst(ClaimTypes.Email);
                //if (emailClaim != null)
                //{
                //    email = emailClaim.Value;
                //}
                
                var result = await _orderService.OrderCompleted(bill, customerId);
                if (result != null)
                {
                    result.CustomerEmail = customerEmail.Trim();
                    _rabbitMQProducer.SendOrderMessage(result);
                    var data = _rabbitMQProducer.ReadMessage(_logger);
                    if (data.Count() > 0)
                    {
                        await _documentService.GenerateInvoiceDocxAsync(data.ToList());

                        return Ok("Saved Successfully");
                    }
                    else
                    {
                        return BadRequest("Issue with RabbitMQ Read Message. Try Again");
                    }
                }
                else
                {
                    return BadRequest("Order is Invalid");
                }
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new(ex);
                return BadRequest(errorResponse);
            }
        }

        [Authorize]
        [HttpPut("UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOrder(OrderViewModel orderViewModel)
        {

            try
            {
                var result = await _orderService.UpdateOrder(orderViewModel);
                if (result != 0)
                {
                    return Ok("Order Not updated");
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
