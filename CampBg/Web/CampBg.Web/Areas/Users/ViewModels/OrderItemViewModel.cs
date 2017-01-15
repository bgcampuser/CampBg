namespace CampBg.Web.Areas.Users.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class OrderItemViewModel
    {
        public static Expression<Func<OrderItem, OrderItemViewModel>> FromOrderItem
        {
            get
            {
                return
                    oi =>
                    new OrderItemViewModel
                        {
                            Id = oi.Id,
                            Name = oi.Product.Name,
                            NameEn = oi.Product.NameEn,
                            Price = oi.Id,
                            Quantity = oi.Quantity,
                            PropertyValues =
                                oi.PropertyValues.AsQueryable()
                                .Where(x => !x.IsDeleted)
                                .Select(ItemPropertyValueViewModel.FromPropertyValue)
                        };
            }
        }

        public static Expression<Func<OrderItem, OrderItemViewModel>> FromOrderItemEn
        {
            get
            {
                return
                    oi =>
                    new OrderItemViewModel
                    {
                        Id = oi.Id,
                        Name = oi.Product.NameEn,
                        Price = oi.Id,
                        Quantity = oi.Quantity,
                        PropertyValues =
                            oi.PropertyValues.AsQueryable()
                            .Where(x => !x.IsDeleted)
                            .Select(ItemPropertyValueViewModel.FromPropertyValueEn)
                    };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public IEnumerable<ItemPropertyValueViewModel> PropertyValues { get; set; }
    }
}