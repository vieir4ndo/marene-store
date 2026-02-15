using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MareneStore.Web.Models
{
    public class CheckoutOrderViewModel
    {
        // Información del carrito
        public ShoppingCart Cart { get; set; }

        // Información del cliente
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string FullName { get; set; }

        // Dirección de envío
        [Required(ErrorMessage = "Shipping address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State/Province is required.")]
        [StringLength(50, ErrorMessage = "State/Province cannot exceed 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(10, ErrorMessage = "Postal code cannot exceed 10 characters.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        public string Country { get; set; }

        public decimal Total { get; set; }
    }
}