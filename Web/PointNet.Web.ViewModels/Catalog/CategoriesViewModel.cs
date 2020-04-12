namespace PointNet.Web.ViewModels.Catalog
{
    using System.Collections.Generic;

    using PointNet.Data.Common.Models;
    using PointNet.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageuUrl { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
