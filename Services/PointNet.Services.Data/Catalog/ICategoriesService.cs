namespace PointNet.Services.Data.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;
    using PointNet.Web.ViewModels.Catalog;

    public interface ICategoriesService
    {
        public CatalogViewModel GetAllCategories();
        public IEnumerable<Category> AllCategoriesList();
    }
}
