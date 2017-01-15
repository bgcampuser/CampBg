namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using CampBg.Web.Areas.Administration.ViewModels;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public class OrderItemsController : AdministrationBaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var orderItems = this.Data.OrderItems.All()
                .AsQueryable()
                .GroupBy(x => x.Product).Select(x => new OrderItemStatisticViewModel
                                                             {
                                                                 ProductId = x.Key.Id,
                                                                 Name = x.Key.Name,
                                                                 Quantity = x.Sum(z => z.Quantity)
                                                             });

            return this.Json(orderItems.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}