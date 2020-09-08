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

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoryService;

        public CategoriesController(
            ICategoriesService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = this.categoryService.GetAllCategories();
            
            return this.View(categories);
        }
    }
}
