namespace CampBg.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using CampBg.Data.Contracts;
    using CampBg.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class CampContext : IdentityDbContext<UserProfile>, ICampContext
    {
        public CampContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Product> Products { get; set; }

        public virtual IDbSet<Manufacturer> Manufacturers { get; set; }

        public virtual IDbSet<Subcategory> Subcategories { get; set; }

        public virtual IDbSet<SubcategoryOption> SubcategoryOptions { get; set; }

        public virtual IDbSet<Property> Properties { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Order> Orders { get; set; }

        public virtual IDbSet<OrderItem> OrderItems { get; set; }

        public virtual IDbSet<SliderImage> SliderImages { get; set; }

        public virtual IDbSet<PropertyValue> PropertyValues { get; set; }

        public IDbSet<StaticPageCategory> StaticPageCateogries { get; set; }

        public IDbSet<StaticPage> StaticPages { get; set; }

        public IDbSet<Newsletter> Newsletters { get; set; }

        public IDbSet<SearchTerm> SearchTerms { get; set; }

        public DbContext Context
        {
            get
            {
                return this;
            }
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyDeleteRules();
            this.ApplyAuditRules();
            return base.SaveChanges();
        }

        private void ApplyDeleteRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                                        .Where(ent => ent.Entity is IDeletable &&
                                            ent.State == EntityState.Deleted))
            {
                var entity = (IDeletable)entry.Entity;
                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }

        private void ApplyAuditRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                                        .Where(ent => ent.Entity is IAuditable &&
                                                        (ent.State == EntityState.Modified ||
                                                        ent.State == EntityState.Added)))
            {
                var entity = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
