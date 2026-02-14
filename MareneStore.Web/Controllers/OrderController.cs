using MareneStore.Application.Interfaces;
using System.Web.Mvc;
using System.Threading.Tasks;
using System;
using MareneStore.Domain.Enums;
using MareneStore.Web.Models;

namespace MareneStore.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int pageNumber = 1, int? pageSize = null)
        {
            if (!pageSize.HasValue)
            {
                pageSize = Session["OrderPageSize"] != null ? (int)Session["OrderPageSize"] : 10;
            }
            else
            {
                Session["OrderPageSize"] = pageSize.Value;
            }
            var userEmail = User.Identity.Name;
            var (orders, totalCount) = await _orderService.GetOrdersByUserEmailAsync(userEmail, pageNumber, pageSize.Value);

            return View(OrderViewModel.FromDto(orders, pageNumber, pageSize.Value, totalCount));
        }

        [HttpPost]
        public async Task<ActionResult> CancelOrder(Guid orderId)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.Cancelled, User.Identity.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditStatus(Guid orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            var model = new EditOrderStatusViewModel
            {
                OrderId = order.Id,
                CurrentStatus = order.Status,
                AvailableStatuses = Enum.GetValues(typeof(OrderStatus))
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditStatus(EditOrderStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableStatuses = Enum.GetValues(typeof(OrderStatus));
                return View(model);
            }

            await _orderService.UpdateOrderStatusAsync(model.OrderId, model.NewStatus);
            return RedirectToAction("Index");
        }
    }
}