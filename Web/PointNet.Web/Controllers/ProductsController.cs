namespace PointNet.Web.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Services.Data;
    using PointNet.Web.ViewModels.Catalog;
    using ReflectionIT.Mvc.Paging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsController : Controller
    {
        private readonly IProductsService productService;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productService,
            IDeletableEntityRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager
            )
        {
            this.productService = productService;
            this.productRepository = productRepository;
            this.userManager = userManager;
        }

        // returns all products from from one category
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

            var currentUser = await userManager.GetUserAsync(this.HttpContext.User);

            await this.productService.AddCommentAsync(productId, commentContent, currentUser.Id);
    
            return RedirectToAction("ProductDetails", new { id = productId });
        }
    }
}
