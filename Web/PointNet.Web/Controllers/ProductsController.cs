namespace PointNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PointNet.Data.Models;
    using PointNet.Services.Data;
    using PointNet.Web.ViewModels.Catalog;

    public class ProductsController : Controller
    {
        private readonly IProductsService productService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productService,
            UserManager<ApplicationUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;
        }

        // returns all products from the same category
        public async Task<IActionResult> Index(int categoryId, int page = 1)
        {
            var products = this.productService.GetProductsInSpecificCategory<ProductViewModel>(categoryId);

            return this.View(await PaginatedList<ProductViewModel>.CreateAsync(products, page, 6));
        }

        public IActionResult ProductDetails(int id)
        {
            var productDetails = this.productService.GetProductDetails(id);
            return this.View(productDetails);
        }

        public async Task<IActionResult> AddComment(int productId, ProductViewModel viewModel)
        {
            string commentContent = viewModel.CommentToAdd;

            var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);

            await this.productService.AddCommentAsync(productId, commentContent, currentUser.Id);

            return this.RedirectToAction("ProductDetails", new { id = productId });
        }
    }
}
