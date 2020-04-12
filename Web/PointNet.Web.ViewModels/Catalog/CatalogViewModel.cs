namespace PointNet.Web.ViewModels.Catalog
{
    using PointNet.Data.Common.Models;
    using PointNet.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CatalogViewModel
    {
        public List<CategoriesViewModel> Categories { get; set; }
    }
}
