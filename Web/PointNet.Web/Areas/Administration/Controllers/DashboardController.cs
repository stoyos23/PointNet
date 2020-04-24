namespace PointNet.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Common;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Administration.Dashboard;
    using PointNet.Web.ViewModels.Catalog;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly ISettingsService settingsService;

        public DashboardController(
            ISettingsService settingsService,
            ICategoriesService categoriesService,
            IProductsService productService)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            var model = new ProductViewModel();
            model.AllCategories = this.categoriesService.AllCategories().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();

            return this.View("AddNewProduct", model);
        }

        [HttpPost]
        public IActionResult AddNewProduct(ProductViewModel viewModel)
        {
            if (this.ModelState.IsValid)
            {
                this.productService.AddNewProduct<ProductViewModel>(viewModel);
                return this.Redirect("/");
            }
            else
            {
                return this.View("AddNewProduct", viewModel);
            }
        }

        [HttpGet]
        public IActionResult FindProducts()
        {
            var selectedProducts = this.productService.GetProductsByName<ProductViewModel>(null);

            if (selectedProducts != null)
            {
                return this.View(selectedProducts.ToList());
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult FindProducts(string productName)
        {
            var selectedProducts = this.productService.GetProductsByName<ProductViewModel>(productName);

            if (selectedProducts != null)
            {
                return this.View(selectedProducts.ToList());
            }

            return this.View();
        }

        public IActionResult RemoveProduct(int productId)
        {
            if(productId != null)
            {
                this.productService.RemoveProduct(productId);
            }

            return this.RedirectToAction("FindProducts");
        }
    }
}
