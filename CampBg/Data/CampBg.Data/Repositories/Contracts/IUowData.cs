namespace CampBg.Data.Repositories.Contracts
{
    using CampBg.Data.Contracts;
    using CampBg.Data.Models;

    public interface IUowData
    {
        IDeletableRepository<Category> Categories { get; }

        IDeletableRepository<Order> Orders { get; }

        IDeletableRepository<OrderItem> OrderItems { get; }

        IDeletableRepository<Product> Products { get; }

        IDeletableRepository<Manufacturer> Manufacturers { get; }

        IDeletableRepository<Property> Properties { get; }

        IDeletableRepository<Subcategory> Subcategories { get; }

        IDeletableRepository<SubcategoryOption> SubcategoryOptions { get; }

        IDeletableRepository<SliderImage> SliderImages { get; }

        IDeletableRepository<PropertyValue> PropertyValues { get; }

        IDeletableRepository<StaticPageCategory> StaticPageCategories { get; }

        IDeletableRepository<StaticPage> StaticPages { get; }

        IDeletableRepository<Newsletter> Newsletters { get; }

        IUsersRepository Users { get; }

        IDeletableRepository<SearchTerm> SearchTerms { get; }

        int SaveChanges();
    }
}
