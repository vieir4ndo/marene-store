using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MareneStore.Application.Dtos;
using MareneStore.Application.Interfaces;
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
        [Authorize(Roles = "Admin")]
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
            var (orders, totalCount) = await _orderService.GetAllAsync(pageNumber, pageSize.Value);

            return View(OrderViewModel.FromDto(orders, pageNumber, pageSize.Value, totalCount));
        }

        [HttpGet]
        public async Task<ActionResult> OrderIndex(int pageNumber = 1, int? pageSize = null)
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
        public async Task<ActionResult> CancelOrder(Guid orderId, string returnAction)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, OrderStatus.Cancelled, User.Identity.Name);
            TempData["Message"] = "Order cancelled successfully.";
            if (returnAction != nameof(Index) && returnAction != nameof(OrderIndex))
            {
                returnAction = nameof(OrderIndex);
            }

            return RedirectToAction(returnAction);
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
            TempData["Message"] = "Order status updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Checkout(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart == null || shoppingCart.Items == null || !shoppingCart.Items.Any())
                {
                    if (Session != null)
                        Session["ToastError"] = "Your cart is empty. Please add products before checkout.";
                    return RedirectToAction("ShopIndex", "Product");
                }

                var checkoutViewModel = new CheckoutOrderViewModel
                {
                    Cart = shoppingCart,
                    Total = shoppingCart.Items.Sum(x => x.Price * x.Quantity),
                    CustomerEmail = User.Identity.Name, 
                    FullName = "", 
                };

                return View(checkoutViewModel);
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session["ToastError"] = "An error occurred while processing checkout.";
                return RedirectToAction("Index", "ShoppingCart");
            }
        }

        // POST: Order/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Checkout(CheckoutOrderViewModel model, ShoppingCart shoppingCart)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Cart = shoppingCart;
                    model.Total = shoppingCart?.Items?.Sum(x => x.Price * x.Quantity) ?? 0;
                    return View(model);
                }

                if (shoppingCart == null || shoppingCart.Items == null || !shoppingCart.Items.Any())
                {
                    if (Session != null)
                        Session["ToastError"] = "Your cart is empty.";
                    return RedirectToAction("ShopIndex", "Product");
                }

                string shippingAddress = $"{model.ShippingAddress}, {model.City}, {model.State} {model.PostalCode}, {model.Country}";

                var orderDto = new OrderDto
                {
                    UserName = model.FullName,
                    UserEmail = model.CustomerEmail,
                    UserShippingAddress = shippingAddress,
                    Details = shoppingCart.Items.Select(item => new OrderDetailDto
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    }).ToList()
                };

                var createdOrder = await _orderService.CreateOrderAsync(orderDto);

                ShoppingCartModelBinder.ClearCart(Session);

                TempData["Message"] = "Order placed successfully!";

                return RedirectToAction("OrderConfirmation", new { id = createdOrder.Id });
            }
            catch (Domain.Exceptions.ValidationException vex)
            {
                if (Session != null)
                    Session["ToastError"] = vex.Message;

                model.Cart = shoppingCart;
                model.Total = shoppingCart?.Items?.Sum(x => x.Price * x.Quantity) ?? 0;
                return View(model);
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session["ToastError"] = "An error occurred while placing your order. Please try again.";

                model.Cart = shoppingCart;
                model.Total = shoppingCart?.Items?.Sum(x => x.Price * x.Quantity) ?? 0;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> OrderConfirmation(Guid id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);

                if (order == null)
                {
                    return HttpNotFound();
                }

                if (!User.IsInRole("Admin") && order.UserEmail != User.Identity.Name)
                {
                    if (Session != null)
                        Session["ToastError"] = "You don't have permission to view this order.";
                    return RedirectToAction("OrderIndex");
                }

                ViewBag.OrderId = order.Id;
                ViewBag.OrderDate = order.Date;
                ViewBag.TotalAmount = order.TotalAmount;
                ViewBag.Status = order.Status;

                return View();
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session["ToastError"] = "An error occurred while loading the order confirmation.";
                return RedirectToAction("OrderIndex");
            }
        }
    }
}