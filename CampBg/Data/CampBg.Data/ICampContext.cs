namespace CampBg.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using CampBg.Data.Models;

    public interface ICampContext : IDisposable
    {
        IDbSet<Product> Products { get; set; }

        IDbSet<Manufacturer> Manufacturers { get; set; }

        IDbSet<Subcategory> Subcategories { get; set; }

        IDbSet<SubcategoryOption> SubcategoryOptions { get; set; }
        
        IDbSet<Property> Properties { get; set; }
        
        IDbSet<Category> Categories { get; set; }
        
        IDbSet<UserProfile> Users { get; set; }
        
        IDbSet<Order> Orders { get; set; }

        IDbSet<OrderItem> OrderItems { get; set; }

        IDbSet<SliderImage> SliderImages { get; set; }

        IDbSet<PropertyValue> PropertyValues { get; set; }

        IDbSet<StaticPageCategory> StaticPageCateogries { get; set; }

        IDbSet<StaticPage> StaticPages { get; set; }

        IDbSet<Newsletter> Newsletters { get; set; }

        IDbSet<SearchTerm> SearchTerms { get; set; }

        int SaveChanges();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
 
        IDbSet<T> Set<T>() where T : class;
    }
}
