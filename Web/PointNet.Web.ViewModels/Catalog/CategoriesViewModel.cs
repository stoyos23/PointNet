namespace PointNet.Web.ViewModels.Catalog
{
    using System.Collections.Generic;

    public class CategoriesViewModel
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string ImageuUrl { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
