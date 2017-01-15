namespace CampBg.Web.Areas.Administration.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using CampBg.Data.Models;

    public class SearchTermViewModel
    {
        public static Expression<Func<SearchTerm, SearchTermViewModel>> FromSearchTerm
        {
            get
            {
                return st => new SearchTermViewModel
                                {
                                    Id = st.Id,
                                    Term = st.Term,
                                    Count = st.Count
                                };
            }
        }

        public int Id { get; set; }

        public string Term { get; set; }

        public int Count { get; set; }
    }
}