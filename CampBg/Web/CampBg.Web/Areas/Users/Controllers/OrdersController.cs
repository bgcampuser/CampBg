namespace CampBg.Web.Areas.Users.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;

    using CampBg.Web.Areas.Users.ViewModels;
    using CampBg.Web.Controllers;

    [Authorize]
    public class OrdersController : BaseController
    {
        public ActionResult Index()
        {
            IEnumerable<OrderViewModel> orders;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                orders =
                    this.Data.Orders.All().Where(x => x.UserId == this.UserProfile.Id).Select(OrderViewModel.FromOrder);
            }
            else
            {
                orders =
                    this.Data.Orders.All()
                        .Where(x => x.UserId == this.UserProfile.Id)
                        .Select(OrderViewModel.FromOrderEn);
            }

            return this.View(orders);
        }
    }
}