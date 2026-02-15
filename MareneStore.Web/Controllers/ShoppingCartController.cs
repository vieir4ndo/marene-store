using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MareneStore.Application.Dtos;
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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(Guid id, int quantity, ShoppingCart shoppingCart)
        {
            try
            {
                // CRÍTICO: Asegurar que shoppingCart y Items no sean null
                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart { Items = new List<ShoppingCart.ShoppingCartItem>() };
                }

                if (shoppingCart.Items == null)
                {
                    shoppingCart.Items = new List<ShoppingCart.ShoppingCartItem>();
                }

                // Validar cantidad mínima
                if (quantity < 1)
                    quantity = 1;

                // Obtener el producto
                var productDto = await _productService.GetByIdAsync(id);

                if (productDto == null)
                {
                    if (Session != null)
                        Session["ToastError"] = "Product not found.";
                    return RedirectToAction("ShopIndex", "Product");
                }

                // Validar que el producto esté activo
                if (!productDto.IsActive.GetValueOrDefault())
                {
                    if (Session != null)
                        Session["ToastError"] = "This product is not available.";
                    return RedirectToAction("ShopIndex", "Product");
                }

                // Buscar si el producto ya está en el carrito
                var existingItem = shoppingCart.Items.FirstOrDefault(x => x.ProductId == id);

                if (existingItem == null)
                {
                    // Validar stock disponible
                    if (quantity > productDto.Stock.GetValueOrDefault())
                    {
                        if (Session != null)
                            Session["ToastError"] = $"Only {productDto.Stock} units available in stock.";
                        return RedirectToAction("ShopIndex", "Product");
                    }

                    // Agregar nuevo producto al carrito
                    shoppingCart.Items.Add(new ShoppingCart.ShoppingCartItem
                    {
                        ProductId = productDto.Id,
                        Name = productDto.Name,
                        Price = productDto.Price ?? 0,
                        ImageUrl = productDto.ImageUrl,
                        Quantity = quantity
                    });

                    TempData["Message"] = $"{productDto.Name} added to cart successfully.";
                }
                else
                {
                    // Validar stock disponible al sumar la cantidad existente
                    int newQuantity = existingItem.Quantity + quantity;
                    if (newQuantity > productDto.Stock.GetValueOrDefault())
                    {
                        if (Session != null)
                            Session["ToastError"] = $"Only {productDto.Stock} units available. You already have {existingItem.Quantity} in cart.";
                        return RedirectToAction("ShopIndex", "Product");
                    }

                    // Incrementar la cantidad
                    existingItem.Quantity += quantity;
                    TempData["Message"] = $"Updated {productDto.Name} quantity in cart.";
                }

                // Guardar el carrito en la sesión
                if (Session != null)
                {
                    Session[ShoppingCartModelBinder.SessionKey] = shoppingCart;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log del error
                if (Session != null)
                    Session["ToastError"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("ShopIndex", "Product");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(Guid id, ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { Items = new List<ShoppingCart.ShoppingCartItem>() };
            }

            if (shoppingCart.Items == null)
            {
                shoppingCart.Items = new List<ShoppingCart.ShoppingCartItem>();
            }

            var item = shoppingCart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                shoppingCart.Items.Remove(item);
                if (Session != null)
                {
                    Session[ShoppingCartModelBinder.SessionKey] = shoppingCart;
                }
                TempData["Message"] = "Product removed from cart.";
            }

            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateQuantity(Guid id, int quantity, ShoppingCart shoppingCart)
        {
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { Items = new List<ShoppingCart.ShoppingCartItem>() };
            }

            if (shoppingCart.Items == null)
            {
                shoppingCart.Items = new List<ShoppingCart.ShoppingCartItem>();
            }

            var item = shoppingCart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item != null)
            {
                if (quantity < 1)
                {
                    if (Session != null)
                        Session["ToastError"] = "Quantity must be at least 1.";
                    return RedirectToAction("Index");
                }

                // Validar stock disponible
                var productDto = await _productService.GetByIdAsync(id);
                if (quantity > productDto.Stock.GetValueOrDefault())
                {
                    if (Session != null)
                        Session["ToastError"] = $"Only {productDto.Stock} units available in stock.";
                    return RedirectToAction("Index");
                }

                item.Quantity = quantity;
                if (Session != null)
                {
                    Session[ShoppingCartModelBinder.SessionKey] = shoppingCart;
                }
                TempData["Message"] = "Cart updated successfully.";
            }

            return RedirectToAction("Index");
        }

        // POST: ShoppingCart/Clear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear()
        {
            if (Session != null)
            {
                Session.Remove(ShoppingCartModelBinder.SessionKey);
            }
            TempData["Message"] = "Cart cleared successfully.";
            return RedirectToAction("Index");
        }
    }

}