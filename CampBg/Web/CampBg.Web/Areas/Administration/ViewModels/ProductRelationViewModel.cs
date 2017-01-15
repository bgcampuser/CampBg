namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class ProductRelationViewModel
    {
        public static Expression<Func<Product, ProductRelationViewModel>> FromProduct
        {
            get
            {
                return pr => new ProductRelationViewModel
                                 {
                                     RelationId = pr.Id,
                                     Name = pr.Name,
                                     NameEn = pr.NameEn
                                 };
            }
        }

        public int RelationId { get; set; }

        public string Name { get; set; }

        public string NameEn { get; set; }
    }
}