 namespace PointNet.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Services.Data.ShoppingCart;
    using PointNet.Web.ViewModels.ShoppingCart;

    public class ShoppingCartController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(
            UserManager<ApplicationUser> userManager,
            IShoppingCartService shoppingCartService)
        {
            this.userManager = userManager;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            var shoppingCartItems = this.shoppingCartService.GetCart(user.Id);
            var shoppingCartViewModel = new ShoppingCartViewModel();

            if (shoppingCartItems != null)
            {
                shoppingCartViewModel.ShoppingCartItems = shoppingCartItems;

                return this.View(shoppingCartViewModel);
            }
            else
            {
                return this.View();
            }
        }

        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            await this.shoppingCartService.AddToCartAsync(id, user.Id, quantity);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.HttpContext.User);
            await this.shoppingCartService.RemoveFromCartAsync(id, user.Id);

            return this.RedirectToAction("Index");
        }
    }
}
