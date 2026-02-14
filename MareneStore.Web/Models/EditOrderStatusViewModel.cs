using MareneStore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MareneStore.Web.Models
{
    public class EditOrderStatusViewModel
    {
        public Guid OrderId { get; set; }

        [Display(Name = "Current Status")]
        public OrderStatus CurrentStatus { get; set; }

        [Required]
        [Display(Name = "New Status")]
        public OrderStatus NewStatus { get; set; }

        public Array AvailableStatuses { get; set; }
    }
}