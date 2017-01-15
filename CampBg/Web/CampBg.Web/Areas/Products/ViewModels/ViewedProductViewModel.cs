namespace CampBg.Web.Areas.Products.ViewModels
{
    using System.Collections.Generic;

    public class ViewedProductViewModel
    {
        private readonly LinkedList<int> viewedProductIds;

        private readonly int size;

        public ViewedProductViewModel() : this(4)
        {
            var a = new Stack<int>();
        }

        public ViewedProductViewModel(int size)
        {
            this.size = size;
            this.viewedProductIds = new LinkedList<int>();
        }

        public IEnumerable<int> ProductIds
        {
            get
            {
                return this.viewedProductIds;
            }
        }

        public void Add(int id)
        {
            if (this.viewedProductIds.Count == this.size)
            {
                this.viewedProductIds.RemoveFirst();
            }

            this.viewedProductIds.AddLast(id);
        }
    }
}