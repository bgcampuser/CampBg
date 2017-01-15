namespace CampBg.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using CampBg.Data.Contracts;
    using CampBg.Data.Models;
    using CampBg.Data.Repositories;
    using CampBg.Data.Repositories.Contracts;

    using IUsersRepository = CampBg.Data.Repositories.Contracts.IUsersRepository;

    public class UowData : IUowData
    {
        private readonly DbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new CampContext())
        {
        }

        public UowData(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context", "Context cannot be null");
            }

            this.context = context;
        }

        public IDeletableRepository<Category> Categories
        {
            get
            {
                return this.GetDeletableRepository<Category>();
            }
        }

        public IDeletableRepository<Order> Orders
        {
            get
            {
                return this.GetDeletableRepository<Order>();
            }
        }

        public IDeletableRepository<OrderItem> OrderItems
        {
            get
            {
                return this.GetDeletableRepository<OrderItem>();
            }
        }

        public IDeletableRepository<Product> Products
        {
            get
            {
                return this.GetDeletableRepository<Product>();
            }
        }

        public IDeletableRepository<Manufacturer> Manufacturers
        {
            get
            {
                return this.GetDeletableRepository<Manufacturer>();
            }
        }

        public IDeletableRepository<Property> Properties
        {
            get
            {
                return this.GetDeletableRepository<Property>();
            }
        }

        public IDeletableRepository<Subcategory> Subcategories
        {
            get
            {
                return this.GetDeletableRepository<Subcategory>();
            }
        }

        public IDeletableRepository<SubcategoryOption> SubcategoryOptions
        {
            get
            {
                return this.GetDeletableRepository<SubcategoryOption>();
            }
        }

        public IDeletableRepository<SliderImage> SliderImages
        {
            get
            {
                return this.GetDeletableRepository<SliderImage>();
            }
        }

        public IDeletableRepository<PropertyValue> PropertyValues
        {
            get
            {
                return this.GetDeletableRepository<PropertyValue>();
            }
        }

        public IDeletableRepository<StaticPageCategory> StaticPageCategories
        {
            get
            {
                return this.GetDeletableRepository<StaticPageCategory>();
            }
        }

        public IDeletableRepository<StaticPage> StaticPages
        {
            get
            {
                return this.GetDeletableRepository<StaticPage>();
            }
        }

        public IDeletableRepository<Newsletter> Newsletters
        {
            get
            {
                return this.GetDeletableRepository<Newsletter>();
            }
        }

        public IUsersRepository Users
        {
            get
            {
                return (UsersRepository)this.GetDeletableRepository<UserProfile>();
            }
        }

        public IDeletableRepository<SearchTerm> SearchTerms
        {
            get
            {
                return this.GetDeletableRepository<SearchTerm>();
            }
        } 

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        private IDeletableRepository<T> GetDeletableRepository<T>()
            where T : class, IDeletable
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                if (typeof(T).IsAssignableFrom(typeof(UserProfile)))
                {
                    var usersRepository = Activator.CreateInstance(typeof(UsersRepository), this.context);
                    this.repositories.Add(typeof(T), usersRepository);
                }
                else
                {
                    var repository = Activator.CreateInstance(typeof(GenericDeletableRepository<T>), this.context);
                    this.repositories.Add(typeof(T), repository);
                }
            }

            return (IDeletableRepository<T>)this.repositories[typeof(T)];
        }
    }
}
