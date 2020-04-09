namespace PointNet.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Models;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Web.ViewModels.Catalog;

    public class ManagerController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productService;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ManagerController(
            ICategoriesService categoriesService,
            IProductsService productService,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.productService = productService;
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        public async Task<IActionResult> AddNewRole()
        {
            await this.roleManager.CreateAsync(new ApplicationRole
            {
                Name = "Manager",
            });
            var user = await this.userManager.GetUserAsync(this.User);
            await this.userManager.AddToRoleAsync(user, "Manager");
            return this.Ok();
        }
    }
}
