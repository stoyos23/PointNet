namespace PointNet.Web.ViewModels.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CatalogViewModel
    {
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
