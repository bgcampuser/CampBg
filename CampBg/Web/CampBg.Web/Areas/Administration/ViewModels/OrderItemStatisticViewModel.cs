namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class OrderItemStatisticViewModel
    {
        public static Expression<Func<OrderItem, OrderItemStatisticViewModel>> FromOrderItem
        {
            get
            {
                return oi => new OrderItemStatisticViewModel
                                 {
                                     ProductId = oi.ProductId,
                                     Name = oi.Product.Name,
                                     Quantity = oi.Quantity
                                 };
            }
        }

        public int ProductId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}