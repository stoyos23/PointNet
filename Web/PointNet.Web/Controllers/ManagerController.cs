namespace PointNet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data.Common.Models;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Catalog;

    public class ManagerController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private IProductsService productService;

        public ManagerController(ICategoriesService categoriesService, IProductsService productService)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            var model = new ProductViewModel();
            model.AllCategories = categoriesService.AllCategories().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        

            return this.View("AddNewProduct", model);
        }

        [HttpPost]
        public IActionResult AddNewProduct(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                this.productService.AddNewProduct<ProductViewModel>(viewModel);
                return this.Redirect("/");
            }
            else
            {
                return this.View("AddNewProduct", viewModel);
            }
        }

        //private IEnumerable<SelectListItem> GetAllCategories()
        //{
        //    var categories = this.categoriesService.AllCategories()
        //                .Select(x =>
        //                        new SelectListItem
        //                        {
        //                            Value = x.Id.ToString(),
        //                            Text = x.Name,
        //                        });

        //    return new SelectList(categories, "Value", "Text");
        //}
    }
}
