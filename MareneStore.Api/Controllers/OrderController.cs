
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
        public async Task<IHttpActionResult> CreateOrder([FromBody] OrderRequest request)
        {
            request.ValidateCreation();
            var order = await _orderService.CreateOrderAsync(request.ToDto());
            return Ok(ApiResponse.CreateSuccess(order));
        }

        //TODO: Add pagination to GetAllOrders
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(ApiResponse.CreateSuccess(orders));
        }

        [HttpGet]
        [Route("{orderId}")]
        public async Task<IHttpActionResult> GetOrderById(Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            return Ok(ApiResponse.CreateSuccess(order));
        }

        //TODO: Validate if delete is correct verb for canceling an order
        [HttpDelete]
        [Route("{orderId}")]
        public async Task<IHttpActionResult> CancelOrder(Guid orderId)
        {
            var order = await _orderService.CancelOrderAsync(orderId);
            return Ok(ApiResponse.CreateSuccess(order));
        }

        //TODO: Implement missing status updates

    }
}