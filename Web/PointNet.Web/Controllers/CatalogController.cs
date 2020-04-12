namespace PointNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Catalog;
    using PointNet.Services.Mapping;

    // TODO: Remove Db Context and replace with respositories
    public class CatalogController : Controller
    {
        private readonly IProductsService productService;
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesService categoriesService;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CatalogController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            ApplicationDbContext dbContext,
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Category> categoryRepository)
        {
            this.productService = productsService;
            this.dbContext = dbContext;
            this.categoriesService = categoriesService;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new CatalogViewModel();


            //var categories = this.dbContext.Categories.Select(x => new CategoriesViewModel
            //{
            //    Name = x.Name,
            //    ImageuUrl = x.ImageuUrl,
            //    CategoryId = x.Id,
            //}).ToList();
            //viewModel.Categories = categories;

            var categories = this.categoryRepository.All().To<CategoriesViewModel>();
            viewModel.Categories = categories.ToList();

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

        public IActionResult ProductDetails(int id)
        {
            var productDetails = this.productService.GetProductDetails(id);
            return this.View(productDetails);
        }
    }
}
