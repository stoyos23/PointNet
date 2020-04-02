namespace PointNet.Services.Data.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;

    public interface ICategoriesService
    {
        public IEnumerable<Category> AllCategories();
    }
}
