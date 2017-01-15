namespace CampBg.Web.Areas.Users.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class OrderViewModel
    {
        private int id;

        public static Expression<Func<Order, OrderViewModel>> FromOrder
        {
            get
            {
                return
                    o =>
                    new OrderViewModel
                        {
                            Id = o.Id,
                            OrderItems =
                                o.OrderItems.AsQueryable()
                                .Where(x => !x.IsDeleted)
                                .Select(OrderItemViewModel.FromOrderItem)
                        };
            }
        }

        public static Expression<Func<Order, OrderViewModel>> FromOrderEn
        {
            get
            {
                return
                    o =>
                    new OrderViewModel
                    {
                        Id = o.Id,
                        OrderItems =
                            o.OrderItems.AsQueryable()
                            .Where(x => !x.IsDeleted)
                            .Select(OrderItemViewModel.FromOrderItemEn)
                    };
            }
        }

        public int Id
        {
            get
            {
                return this.id + 1000;
            }

            set
            {
                this.id = value;
            }
        }

        public decimal Total
        {
            get
            {
                return this.OrderItems.Sum(x => x.Price * x.Quantity);
            }
        }

        public IEnumerable<OrderItemViewModel> OrderItems { get; set; }
    }
}