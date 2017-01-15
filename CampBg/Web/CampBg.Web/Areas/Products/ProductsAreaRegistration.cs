namespace CampBg.Web.Areas.Products
{
    using System.Web.Mvc;

    public class ProductsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Products";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Product_details",
                "Products/Details/{id}/{name}/",
                new
                    {
                        action = "Details",
                        controller = "Products",
                        id = UrlParameter.Optional,
                        name = UrlParameter.Optional,
                        area = "Products"
                    });

            context.MapRoute(
                "Product_search",
                "Products/Search",
                new
                {
                    action = "Search",
                    controller = "Products",
                    area = "Products"
                });

            context.MapRoute(
                "Products_SubcategoryOptions_Filter",
                "Products/{category}/{subcategory}/{subcategoryOption}/Filter",
                new
                {
                    action = "Filter",
                    controller = "Products",
                    subcategory = UrlParameter.Optional,
                    category = UrlParameter.Optional,
                    subcategoryOption = UrlParameter.Optional,
                    area = "Products"
                });

            context.MapRoute(
                "Products_ByManufacturer",
                "Products/ByManufacturer/{name}",
                new
                {
                    action = "ByManufacturer",
                    controller = "Products",
                    name = UrlParameter.Optional,
                    area = "Products"
                });

            context.MapRoute(
                "Products_Manufacturers_Filter",
                "Products/ByManufacturer/{name}/Filter",
                new
                {
                    action = "ByManufacturer",
                    controller = "Products",
                    name = UrlParameter.Optional,
                    area = "Products"
                });

            context.MapRoute(
                    "Products_Subcategory_Filter",
                    "Products/{category}/{subcategory}/Filter",
                    new
                    {
                        action = "Filter",
                        controller = "Products",
                        subcategory = UrlParameter.Optional,
                        category = UrlParameter.Optional,
                        subcategoryOption = UrlParameter.Optional,
                        area = "Products"
                    });

            context.MapRoute(
                "Products_bySubcategory",
                "Products/{category}/{name}/",
                new
                {
                    action = "BySubcategory",
                    controller = "Products",
                    name = UrlParameter.Optional,
                    category = UrlParameter.Optional,
                    area = "Products"
                });

            context.MapRoute(
                "Products_bySubcategoryOption",
                "Products/{category}/{subcategory}/{subcategoryOption}/",
                new
                {
                    action = "BySubcategoryOption",
                    controller = "Products",
                    subcategoryOption = UrlParameter.Optional,
                    category = UrlParameter.Optional,
                    subcategory = UrlParameter.Optional,
                    area = "Products"
                });
        }
    }
}