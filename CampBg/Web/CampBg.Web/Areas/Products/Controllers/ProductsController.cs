namespace CampBg.Web.Areas.Products.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;

    using CampBg.Data.Models;
    using CampBg.Extensions;
    using CampBg.Web.Areas.Products.ViewModels;
    using CampBg.Web.Controllers;
    using CampBg.Web.ViewModels;

    public class ProductsController : BaseController
    {
        private const int PageSize = 12;

        private const int FilterPageSize = 12;

        private const string LastViewed = "lastViewed";

        public ActionResult Details(int id, string name)
        {
            var lastViewed = (ViewedProductViewModel)this.Session[LastViewed] ?? new ViewedProductViewModel();

            ProductDetailsViewModel product;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                this.ViewBag.LastViewed =
                    this.Data.Products.All()
                        .Where(x => lastViewed.ProductIds.Contains(x.Id))
                        .Select(ProductListViewModel.FromProduct);

                product =
                    this.Data.Products.All()
                        .Where(prod => prod.Id == id)
                        .Select(ProductDetailsViewModel.FromProduct)
                        .FirstOrDefault();
            }
            else
            {
                this.ViewBag.LastViewed =
                    this.Data.Products.All()
                        .Where(x => lastViewed.ProductIds.Contains(x.Id))
                        .Select(ProductListViewModel.FromProductEn);

                product =
                    this.Data.Products.All()
                        .Where(prod => prod.Id == id)
                        .Select(ProductDetailsViewModel.FromProductEn)
                        .FirstOrDefault();
            }

            lastViewed.Add(id);
            this.Session[LastViewed] = lastViewed;

            return this.View(product);
        }

        public ActionResult ByManufacturer(string name, int page = 0)
        {
            FilterPageViewModel model;

            var products =
                this.Data.Products.All()
                    .Where(
                        x =>
                        x.Manufacturer.Name == name);

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                model = new FilterPageViewModel
                {
                    InitialProducts = products
                                    .Select(ProductListViewModel.FromProduct)
                                    .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.Name,
                                PropertyId = x.Key.Id,
                                Values =
                                        x.Distinct()
                                        .OrderBy(v => v.Value)
                                        .Select(
                                            z =>
                                            new PropertyValueViewModel
                                            {
                                                Id = z.Id,
                                                Value = z.Value
                                            })
                            })
                        .ToList();
            }
            else
            {
                model = new FilterPageViewModel
                {
                    InitialProducts = products
                        .Select(ProductListViewModel.FromProductEn)
                        .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.NameEn,
                                PropertyId = x.Key.Id,
                                Values =
                                        x.Distinct()
                                        .OrderBy(v => v.ValueEn)
                                        .Select(
                                            z =>
                                            new PropertyValueViewModel
                                            {
                                                Id = z.Id,
                                                Value = z.ValueEn
                                            })
                            })
                        .ToList();
            }

            model.Filters.PriceFilter = new PriceViewModel
            {
                Maximum = products.Any() ? products.Max(z => z.Price) : 0m,
                Minimum = products.Any() ? products.Min(z => z.Price) : 0m,
            };

            this.ViewBag.Category = name;
            model.InitialProducts = model.InitialProducts.Skip(page * PageSize).Take(PageSize);
            this.AttachPagination(products, page);

            return this.View(model);
        }

        public ActionResult BySubcategory(string name, string category, int page = 0)
        {
            IQueryable<Product> products;

            FilterPageViewModel model;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                products = this.Data.Products.All()
                                .Where(
                                    x =>
                                    x.Subcategory.Name == name
                                    && x.Subcategory.Category.Name == category);

                model = new FilterPageViewModel
                {
                    InitialProducts =
                                    GetProductsInSubcategory(name, category)
                                    .Select(ProductListViewModel.FromProduct)
                                    .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.Name,
                                PropertyId = x.Key.Id,
                                Values =
                                        x.Distinct()
                                        .OrderBy(v => v.Value)
                                        .Select(
                                            z =>
                                            new PropertyValueViewModel
                                            {
                                                Id = z.Id,
                                                Value = z.Value
                                            })
                            })
                        .ToList();
            }
            else
            {
                products = this.Data.Products.All()
                                .Where(
                                    x =>
                                    x.Subcategory.NameEn == name
                                    && x.Subcategory.Category.NameEn == category);

                model = new FilterPageViewModel
                {
                    InitialProducts =
                        this.GetProductsInSubcategory(name, category)
                        .Select(ProductListViewModel.FromProductEn)
                        .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.NameEn,
                                PropertyId = x.Key.Id,
                                Values =
                                    x.Distinct()
                                    .OrderBy(v => v.ValueEn)
                                    .Select(
                                        z =>
                                        new PropertyValueViewModel
                                        {
                                            Id = z.Id,
                                            Value = z.ValueEn
                                        })
                            })
                        .ToList();
            }

            model.Filters.ManufacturerFilter =
                this.Data.Manufacturers.All()
                    .Where(
                        x =>
                        !x.IsDeleted
                        && x.Products.Any(
                            z =>
                            (z.Subcategory.Category.Name == category
                            && z.Subcategory.Name == name) ||
                            (z.Subcategory.Category.NameEn == category
                            && z.Subcategory.NameEn == name)))
                    .Select(ManufacturerViewModel.FromManufacturer);

            model.Filters.PriceFilter = new PriceViewModel
            {
                Maximum = products.Any() ? products.Max(z => z.Price) : 0m,
                Minimum = products.Any() ? products.Min(z => z.Price) : 0m,
            };

            this.ViewBag.Category = category;
            this.ViewBag.Subcategory = name;

            model.InitialProducts = model.InitialProducts.Skip(page * PageSize).Take(PageSize);
            this.AttachPagination(products, page);

            return this.View(model);
        }

        public ActionResult BySubcategoryOption(string category, string subcategory, string subcategoryOption, int page = 0)
        {
            var products =
                this.Data.Products.All()
                    .Where(
                        x =>
                        x.SubcategoryOption != null &&
                        x.SubcategoryOption.Subcategory.Name == subcategory
                        && x.SubcategoryOption.Subcategory.Category.Name == category
                        && x.SubcategoryOption.Name == subcategoryOption);

            FilterPageViewModel model;

            if (Thread.CurrentThread.CurrentCulture.Name == "bg-BG")
            {
                model = new FilterPageViewModel
                {
                    InitialProducts =
                                    this.GetProductsInSubcategoryOption(
                                        subcategory,
                                        category,
                                        subcategoryOption)
                                        .Select(ProductListViewModel.FromProduct)
                                        .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.Name,
                                PropertyId = x.Key.Id,
                                Values =
                                        x.Distinct()
                                        .OrderBy(v => v.Value)
                                        .Select(
                                            z =>
                                            new PropertyValueViewModel
                                            {
                                                Id = z.Id,
                                                Value = z.Value
                                            })
                            })
                        .ToList();
            }
            else
            {
                model = new FilterPageViewModel
                {
                    InitialProducts =
                        this.GetProductsInSubcategoryOption(
                            subcategory,
                            category,
                            subcategoryOption)
                            .Select(ProductListViewModel.FromProductEn)
                            .OrderBy(x => x.Name),
                    Filters = new FilterContainerViewModel()
                };

                model.Filters.PropertyFilter =
                    products.SelectMany(x => x.PropertyValues)
                        .GroupBy(x => x.Property)
                        .Select(
                            x =>
                            new ProductPropertyViewModel
                            {
                                Name = x.Key.NameEn,
                                PropertyId = x.Key.Id,
                                Values =
                                    x.Distinct()
                                    .OrderBy(v => v.ValueEn)
                                    .Select(
                                        z =>
                                        new PropertyValueViewModel
                                        {
                                            Id = z.Id,
                                            Value = z.ValueEn
                                        })
                            })
                        .ToList();
            }

            model.Filters.ManufacturerFilter =
                this.Data.Manufacturers.All()
                    .Where(
                        x =>
                        !x.IsDeleted
                        && x.Products.Any(
                            z =>
                            z.SubcategoryOption != null &&
                            z.SubcategoryOption.Subcategory.Category.Name == category
                            && z.SubcategoryOption.Subcategory.Name == subcategory
                            && z.SubcategoryOption.Name == subcategoryOption))
                    .Select(ManufacturerViewModel.FromManufacturer);

            var targetProducts = this.GetProductsInSubcategoryOption(subcategory, category, subcategoryOption);

            model.Filters.PriceFilter = new PriceViewModel
            {
                Maximum = targetProducts.Any() ? targetProducts.Max(x => x.Price) : 0m,
                Minimum = targetProducts.Any() ? targetProducts.Min(z => z.Price) : 0m,
            };

            this.ViewBag.Category = category;
            this.ViewBag.Subcategory = subcategory;
            this.ViewBag.SubcategoryOption = subcategoryOption;

            model.InitialProducts = model.InitialProducts.Skip(page * PageSize).Take(PageSize);

            this.AttachPagination(products, page);

            return this.View(model);
        }

        [HttpPost]
        public ActionResult ByManufacturer(FilterViewModel filter, string name, OrderByType orderBy = 0, OrderType orderType = 0, int page = 0)
        {
            var productsToFilter = this.Data.Products.All().Where(x => x.Manufacturer.Name == name);
            var filterResult = this.ApplyFiltering(productsToFilter, filter);
            filterResult = this.ApplyOrderBy(filterResult, orderBy, orderType);

            var filterResultVm =
                filterResult.Skip(FilterPageSize * page).Take(PageSize).Select(ProductListViewModel.FromProduct);

            this.AttachPagination(filterResult, page);

            return this.PartialView("FilterResult", filterResultVm);
        }

        [HttpPost]
        public ActionResult Filter(FilterViewModel filter, string subcategory, string category, string subcategoryOption, OrderByType orderBy = 0, OrderType orderType = 0, int page = 0)
        {
            var productsToFilter = string.IsNullOrEmpty(subcategoryOption)
                                       ? this.GetProductsInSubcategory(subcategory, category)
                                       : this.GetProductsInSubcategoryOption(subcategory, category, subcategoryOption);

            var filterResult = this.ApplyFiltering(productsToFilter, filter);

            filterResult = this.ApplyOrderBy(filterResult, orderBy, orderType);

            var filterResultViewModel = Thread.CurrentThread.CurrentCulture.Name == "bg-BG" ?
                filterResult.Skip(FilterPageSize * page).Take(PageSize).Select(ProductListViewModel.FromProduct) :
                filterResult.Skip(FilterPageSize * page).Take(PageSize).Select(ProductListViewModel.FromProductEn);

            this.AttachPagination(filterResult, page);

            return this.PartialView("FilterResult", filterResultViewModel);
        }

        public ActionResult Search(string q, int page = 0)
        {
            var searchTerms = q.Split(' ').Where(x => x.Length >= 3);
            var searchTermsArray = searchTerms as string[] ?? searchTerms.ToArray();

            foreach (var searchTerm in searchTermsArray)
            {
                var searchTermDb = this.Data.SearchTerms.All().FirstOrDefault(x => x.Term == searchTerm);

                if (searchTermDb == null)
                {
                    searchTermDb = new SearchTerm
                    {
                        Term = searchTerm
                    };

                    this.Data.SearchTerms.Add(searchTermDb);
                }

                searchTermDb.Count++;
            }

            this.Data.SaveChanges();

            int articleNumber;
            int.TryParse(q, out articleNumber);
            articleNumber = articleNumber - 10000;

            var searchResults =
                this.Data.Products.All()
                    .AsQueryable()
                    .Where(x => !x.IsDeleted)
                    .Where(
                        x =>
                        searchTermsArray.Any(term => x.Name.Contains(term))
                        || searchTermsArray.Any(term => x.NameEn.Contains(term))
                        || searchTermsArray.Any(term => x.Description.Contains(term))
                        || searchTermsArray.Any(term => x.DescriptionEn.Contains(term))
                        || searchTermsArray.Any(term => x.Manufacturer.Name == term) || x.Id == articleNumber)
                    .OrderBy(x => x.CreatedOn);

            var searchResultsPaginated =
                searchResults.Skip(page * PageSize).Take(PageSize).Select(ProductListViewModel.FromProduct);

            this.AttachPagination(searchResults, page);
            this.ViewBag.Query = q;

            return this.View(searchResultsPaginated);
        }

        private IQueryable<Product> ApplyOrderBy(IQueryable<Product> filterResult, OrderByType orderBy, OrderType orderType)
        {
            switch (orderBy)
            {
                case OrderByType.ByName:
                    filterResult = orderType == OrderType.Ascending
                                       ? filterResult.OrderBy(x => x.Name)
                                       : filterResult.OrderByDescending(x => x.Name);
                    break;
                case OrderByType.ByPrice:
                    filterResult = orderType == OrderType.Ascending
                                       ? filterResult.OrderBy(x => x.Price)
                                       : filterResult.OrderByDescending(x => x.Price);
                    break;
                case OrderByType.Latest:
                    filterResult = orderType == OrderType.Ascending
                                       ? filterResult.OrderBy(x => x.CreatedOn)
                                       : filterResult.OrderByDescending(x => x.CreatedOn);
                    break;
            }

            return filterResult;
        }

        private IQueryable<Product> GetProductsInSubcategory(string name, string category)
        {
            var result = this.Data.Products.All()
                .Where(x =>
                    (x.Subcategory.Name == name || x.Subcategory.NameEn == name) &&
                    (x.Subcategory.Category.Name == category || x.Subcategory.Category.NameEn == category));

            return result;
        }

        private IQueryable<Product> GetProductsInSubcategoryOption(string name, string category, string subcategoryOption)
        {
            var result =
                this.Data.Products.All()
                    .Where(
                        x =>
                         x.SubcategoryOption != null &&
                        (x.SubcategoryOption.Name == subcategoryOption || x.SubcategoryOption.NameEn == subcategoryOption) &&
                        (x.SubcategoryOption.Subcategory.Name == name || x.SubcategoryOption.Subcategory.NameEn == name) &&
                        (x.SubcategoryOption.Subcategory.Category.Name == category || x.SubcategoryOption.Subcategory.Category.NameEn == category));

            return result;
        }

        private IQueryable<Product> ApplyFiltering(IQueryable<Product> products, FilterViewModel filter)
        {
            if (filter.Manufacturers.Any())
            {
                products =
                    products.Where(
                        LinqBuilder.BuildOrExpression<Product, int>(x => x.ManufacturerId, filter.Manufacturers));
            }

            var groupedFilters = filter.SelectedProperties.GroupBy(x => x.PropertyId);

            products = this.ProcessFilterGroup(products, groupedFilters);

            products = products.Where(x => x.Price >= filter.PriceFilter.MinPrice && x.Price <= filter.PriceFilter.MaxPrice);

            return products;
        }

        private IQueryable<Product> ProcessFilterGroup(
            IQueryable<Product> products,
            IEnumerable<IGrouping<int, PropertyValueViewModel>> groupedFilters)
        {
            foreach (var groupedFilter in groupedFilters)
            {
                var intersection =
                    this.Data.PropertyValues.All()
                        .Where(
                            LinqBuilder.BuildOrExpression<PropertyValue, int>(
                                x => x.Id,
                                groupedFilter.Select(x => x.Id)))
                        .SelectMany(x => x.Products);

                products = products.Intersect(intersection);
            }

            return products;
        }

        private void AttachPagination<T>(IQueryable<T> col, int page)
        {
            this.ViewBag.PageCount = Math.Ceiling((decimal)col.Count() / PageSize);
            this.ViewBag.CurrentPage = page + 1;
        }
    }
}