namespace PointNet.Web.ViewModels.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data.Common.Models;
    using PointNet.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> AllCategories { get; set; }
    }
}
