namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class OrdersController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult GetOrders([DataSourceRequest]DataSourceRequest request)
        {
            var orders = this.Data.Orders.All().Select(OrderViewModel.FromOrder);

            return this.Json(orders.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateOrder([DataSourceRequest] DataSourceRequest request, OrderViewModel model)
        {
            var orders = new List<ProductViewModel>();

            if (this.ModelState.IsValid)
            {
                var order = new Order
                                  {
                                      IsPaid = model.IsPaid,
                                      IsFinalized = model.IsFinalized  
                                  };

                this.Data.Orders.Add(order);
                this.Data.SaveChanges();
            }

            return this.Json(orders.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateOrder([DataSourceRequest] DataSourceRequest request, OrderViewModel model)
        {
            var orders = new List<OrderViewModel> { model };

            var order = this.Data.Orders.GetById(model.Id);
            if (order == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id was provided");
            }

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(order);
                this.Data.SaveChanges();
            }

            return this.Json(orders.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteOrder([DataSourceRequest] DataSourceRequest request, OrderViewModel model)
        {
            var orders = new List<OrderViewModel>();

            var order = this.Data.Orders.GetById(model.Id);
            if (order == null)
            {
                this.ModelState.AddModelError("Id", "Invalid id");
            }

            this.Data.Orders.Delete(order);
            this.Data.SaveChanges();

            return this.Json(orders.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadItems([DataSourceRequest] DataSourceRequest request, int id)
        {
            var order = this.Data.Orders.GetById(id);

            var orderItems = order.OrderItems.AsQueryable().Select(OrderItemViewModel.FromOrderItem);

            return this.Json(orderItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateItems([DataSourceRequest] DataSourceRequest request, OrderItemViewModel model)
        {
            var orderItem = this.Data.OrderItems.GetById(model.Id);

            if (this.ModelState.IsValid)
            {
                this.TryUpdateModel(orderItem);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteItems([DataSourceRequest] DataSourceRequest request, OrderItemViewModel model)
        {
            var orderItem = this.Data.OrderItems.GetById(model.Id);
            var order = this.Data.Orders.GetById(orderItem.Order.Id);

            if (this.ModelState.IsValid)
            {
                order.OrderItems.Remove(orderItem);
                this.Data.SaveChanges();
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, this.ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadItemProperties([DataSourceRequest] DataSourceRequest request, int id)
        {
            var orderItem = this.Data.OrderItems.GetById(id);

            var propertyValues = orderItem.PropertyValues.AsQueryable().Select(OrderItemPropertyViewModel.FromPropertyValue);
            
            return this.Json(propertyValues.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}