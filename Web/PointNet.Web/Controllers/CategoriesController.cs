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
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesController(
            IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var viewModel = new CatalogViewModel();

            var categories = this.categoryRepository.All().To<CategoriesViewModel>();
            viewModel.Categories = categories.ToList();

            return this.View(viewModel);
        }
    }
}
