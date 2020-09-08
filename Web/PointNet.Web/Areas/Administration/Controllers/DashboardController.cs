namespace PointNet.Web.Areas.Administration.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Common;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Catalog;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class DashboardController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly IHostingEnvironment hostingEnvironment;

        public DashboardController(
            ICategoriesService categoriesService,
            IProductsService productService,
            IHostingEnvironment hostingEnvironment)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddNewProduct()
        {
            var model = new ProductViewModel();
            model.AllCategories = this.categoriesService.AllCategoriesList().Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();

            return this.View("AddNewProduct", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(ProductViewModel viewModel)
        {
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
            var imgFile = viewModel.ImageFile;

            if (this.ModelState.IsValid)
            {
                if (imgFile.Length > 0)
                {
                    var fileName = String.Concat(viewModel.Id.ToString(), imgFile.FileName);
                    viewModel.ImageName = fileName;

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await imgFile.CopyToAsync(fileStream);
                    }
                }

                await this.productService.AddNewProductAsync<ProductViewModel>(viewModel);
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

        public async Task<IActionResult> RemoveProduct(int productId)
        {
            if (productId > -1)
            {
                await this.productService.RemoveProductAsync(productId);
            }

            return this.RedirectToAction("FindProducts");
        }
    }
}
