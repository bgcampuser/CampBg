namespace CampBg.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using CampBg.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class DefaultDbConfiguration : DbMigrationsConfiguration<CampContext>
    {
        private readonly List<Tuple<string, string>> staticPageCategoryTitles = new List<Tuple<string, string>>
                                                                      {
                                                                          new Tuple<string, string>("Пазаруване", "Shopping"),
                                                                          new Tuple<string, string>("Помощ", "Help"),
                                                                          new Tuple<string, string>("Начини на плащане", "Payment methods"),
                                                                      };

        private const string AdministratorRoleString = "Administrator";
        private const string OperatorRoleString = "Operator";

        public DefaultDbConfiguration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CampContext context)
        {
            var staticPageCategories = context.StaticPageCateogries;
            if (!staticPageCategories.Any())
            {
                foreach (var staticPageCategory in this.staticPageCategoryTitles)
                {
                    staticPageCategories.Add(
                        new StaticPageCategory { NameBg = staticPageCategory.Item1, NameEn = staticPageCategory.Item2 });
                }
            }

            var roles = context.Roles;
            var administratorRole = roles.FirstOrDefault(x => x.Name == AdministratorRoleString);

            if (administratorRole == null)
            {
                administratorRole = new IdentityRole(AdministratorRoleString);
                roles.Add(administratorRole);
            }

            var operatorRole = roles.FirstOrDefault(x => x.Name == OperatorRoleString);

            if (operatorRole == null)
            {
                operatorRole = new IdentityRole(OperatorRoleString);
                roles.Add(operatorRole);
            }

            var adminAccount = context.Users.FirstOrDefault(x => x.UserName == "admin");
            if (adminAccount == null)
            {
                adminAccount = new UserProfile
                {
                    UserName = "admin",
                    Email = "dininski@gmail.com",
                };

                context.Users.Add(adminAccount);
            }

            if (!adminAccount.Roles.Any(x => x.RoleId == administratorRole.Id))
            {
                adminAccount.Roles.Add(new IdentityUserRole
                                           {
                                               RoleId = administratorRole.Id,
                                               UserId = adminAccount.Id
                                           });
            }

            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method 
            // to avoid creating duplicate seed data. E.g.
            //
            //   context.People.AddOrUpdate(
            //     p => p.FullName,
            //     new Person { FullName = "Andrew Peters" },
            //     new Person { FullName = "Brice Lambson" },
            //     new Person { FullName = "Rowan Miller" }
            //   );
        }
    }
}
