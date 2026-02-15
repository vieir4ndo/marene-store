using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MareneStore.Application.Interfaces;
using MareneStore.Domain.Entities;
using MareneStore.Web.Models;


namespace MareneStore.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {

        private readonly IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index(ShoppingCart shoppingCart)
        {
            var carrito = (ShoppingCart)Session[ShoppingCartModelBinder.SessionKey];

            if (carrito == null)
            {
                carrito = new ShoppingCart();
            }
            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Guid id, int quantity, ShoppingCart shoppingCart)
        {
            if (quantity < 1)
                quantity = 1;

            var existingItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == id);

            if (existingItem == null)
            {
                var productDto = await _productService.GetByIdAsync(id);
                shoppingCart.Items.Add(new ShoppingCart.ShoppingCartItem
                {
                    ProductId = productDto.Id,
                    Name = productDto.Name,
                    Price = productDto.Price ?? 0,
                    Quantity = quantity
                });
            }
            else
            {
                existingItem.Quantity += quantity;
            }

            return RedirectToAction("Index");
        }
    }
}