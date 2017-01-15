namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CampBg.Data.Models;

    public class OrderItemViewModel
    {
        public static Func<OrderItem, OrderItemViewModel> FromOrderItem
        {
            get
            {
                return item => new OrderItemViewModel
                {
                    Id = item.Id,
                    ItemId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    ItemName = item.Product.Name,
                    Properties = item.PropertyValues.AsQueryable().Select(OrderItemPropertyViewModel.FromPropertyValue)
                };
            }
        }

        public int Id { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public IEnumerable<OrderItemPropertyViewModel> Properties { get; set; }
    }
}