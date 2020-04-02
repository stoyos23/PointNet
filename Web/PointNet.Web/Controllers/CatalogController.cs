namespace PointNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Catalog;

    public class CatalogController : Controller
    {
        private readonly IProductsService productService;
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesService categoriesService;

        public CatalogController(IProductsService productsService, ICategoriesService categoriesService, ApplicationDbContext dbContext)
        {
            this.productService = productsService;
            this.dbContext = dbContext;
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var viewModel = new CatalogViewModel();
            var categories = this.dbContext.Categories.Select(x => new CategoriesViewModel
            {
                Name = x.Name,
                ImageuUrl = x.ImageuUrl,
                CategoryId = x.Id,
            }).ToList();
            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        public IActionResult ListProductsInCategory(int id)
         {
            // var products = new CategoriesViewModel
            // {
            //    Products = this.productService.GetProductsInSpecificCategory<ProductViewModel>(id).ToList(),
            // };
            var products = this.productService.GetProductsInSpecificCategory<ProductViewModel>(id);
            return this.View("ListProductsInCategory", products);
        }
    }
}
