namespace PointNet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Session;
    using Microsoft.Extensions.DependencyInjection;
    using PointNet.Data;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Services.Data.ShoppingCart;

    public class ShoppingCartController : Controller
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IDeletableEntityRepository<Product> productRepository, UserManager<ApplicationUser> userManager, IShoppingCartService shoppingCartService)
        {
            this.productRepository = productRepository;
            this.userManager = userManager;
            this.shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var test = user.ShoppingCart.ShoppingCartItems.Count();
            var testId = user.ShoppingCartId;

            this.shoppingCartService.AddToCart(id, user);

            return this.Redirect("/");
        }

        public async Task<IActionResult> GetCart()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var cartProducts = this.shoppingCartService.GetCart(user);

            return this.View(cartProducts);
        }
    }
}
