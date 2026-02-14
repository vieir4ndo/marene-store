
using MareneStore.Api.Requests;
using MareneStore.Api.Responses;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Enums;
using MareneStore.Domain.Exceptions;
using System;
using System.Security.Claims;
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
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.FindFirst(ClaimTypes.Email)?.Value;
            var userName = identity.FindFirst(ClaimTypes.Name)?.Value;
            var order = await _orderService.CreateOrderAsync(request.ToDto(userEmail, userName));
            return Ok(ApiResponse.CreateSuccess(order));
        }

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetAllOrders([FromUri] int pageNumber = 1, [FromUri] int pageSize = 10)
        {
            var (orders, totalCount) = await _orderService.GetAllAsync(pageNumber, pageSize);
            return Ok(ApiResponse.CreateSuccess(orders, pageNumber, pageSize, totalCount));
        }

        [HttpGet]
        [Route("my")]
        [Authorize]
        public async Task<IHttpActionResult> GetMyOrders([FromUri] int pageNumber = 1, [FromUri] int pageSize = 10)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.FindFirst(ClaimTypes.Email)?.Value;
            var (orders, totalCount) = await _orderService.GetOrdersByUserEmailAsync(userEmail, pageNumber, pageSize);
            return Ok(ApiResponse.CreateSuccess(orders, pageNumber, pageSize, totalCount));
        }

        [HttpGet]
        [Route("{orderId}")]
        [Authorize]
        public async Task<IHttpActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.FindFirst(ClaimTypes.Email)?.Value;
            if (order.UserEmail != userEmail)
            {
                throw new UnauthorizedException("You are not authorized to access this order");
            }
            return Ok(ApiResponse.CreateSuccess(order));
        }

        [HttpPatch]
        [Route("{orderId}")]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusRequest request)
        {
            request.Validate();
            var order = await _orderService.UpdateOrderStatusAsync(orderId, request.Status);
            return Ok(ApiResponse.CreateSuccess(order));
        }

        [HttpPatch]
        [Route("{orderId}/cancel")]
        [Authorize]
        public async Task<IHttpActionResult> CancelOrder(Guid orderId)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userEmail = identity.FindFirst(ClaimTypes.Email)?.Value;
            var order = await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.Cancelled, userEmail);
            return Ok(ApiResponse.CreateSuccess(order));
        }
    }
}