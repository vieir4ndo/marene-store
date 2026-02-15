using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MareneStore.Domain.Entities;
using static MareneStore.Web.Models.ShoppingCart;

namespace MareneStore.Web.Models
{
    public class ShoppingCartModelBinder : IModelBinder
    {

        private const string CART_SESSION_KEY = "carrito";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            List<ShoppingCartItem> shoppingCart =
               (List<ShoppingCartItem>)controllerContext.HttpContext
                                           .Session[CART_SESSION_KEY];
            if (shoppingCart == null)
            {
                shoppingCart = new List<ShoppingCartItem>();
                controllerContext.HttpContext
                                .Session[CART_SESSION_KEY] = shoppingCart;
            }

            return shoppingCart;
        }

        /// <summary>
        /// Guarda el carrito en la sesión (método auxiliar opcional)
        /// </summary>
        public static void SaveCart(System.Web.HttpSessionStateBase session, Dictionary<Product, int> carrito)
        {
            if (session != null && carrito != null)
            {
                session[CART_SESSION_KEY] = carrito;
            }
        }

        /// <summary>
        /// Elimina el carrito de la sesión
        /// </summary>
        public static void ClearCart(System.Web.HttpSessionStateBase session)
        {
            if (session != null)
            {
                session.Remove(CART_SESSION_KEY);
            }
        }

        /// <summary>
        /// Obtiene la clave de sesión del carrito
        /// </summary>
        public static string SessionKey
        {
            get { return CART_SESSION_KEY; }
        }
    }

}