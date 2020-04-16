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
    using PointNet.Services.Mapping;
    using PointNet.Web.ViewModels.Catalog;

    // TODO: Remove Db Context and replace with respositories
    public class CategoriesController : Controller
    {
        private readonly IProductsService productService;
        private readonly ApplicationDbContext dbContext;
        private readonly ICategoriesService categoriesService;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesController(
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
    }
}
