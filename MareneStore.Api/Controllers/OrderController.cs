
using MareneStore.Api.Requests;
using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace MareneStore.Api.Controllers
{
    [RoutePrefix("api/v1/orders")]
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IHttpActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            request.ValidateCreation();
            var order = await _orderService.CreateOrderAsync(request.ToDto());
            return Ok(ApiResponse.CreateSuccess(order));
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetAllOrders([FromUri] int pageNumber = 1, [FromUri] int pageSize = 10)
        {
            var orders = await _orderService.GetAllAsync(pageNumber, pageSize);
            return Ok(ApiResponse.CreateSuccess(orders, pageNumber, pageSize));
        }

        [HttpGet]
        [Route("{orderId}")]
        [Authorize]
        //TODO: Add authorization check to ensure users can only access their own orders unless they are admins
        //TODO: Add another get all to get all orders for the currently authenticated user
        public async Task<IHttpActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            return Ok(ApiResponse.CreateSuccess(order));
        }

        [HttpPatch]
        [Route("{orderId}")]
        [Authorize]
        //TODO: Add authorization check to ensure users can only update their own orders unless they are admins
        // Also validate that users can only update to certain statuses (e.g. users can only cancel their own orders, but not mark them as shipped)
        public async Task<IHttpActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
        {
            request.Validate();
            var order = await _orderService.UpdateOrderStatusAsync(orderId, request.Status);
            return Ok(ApiResponse.CreateSuccess(order));
        }
    }
}