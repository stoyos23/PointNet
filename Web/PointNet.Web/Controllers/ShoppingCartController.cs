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
    using PointNet.Services.Data.SessionHelper;
    using PointNet.Services.Data.ShoppingCart;
    using PointNet.Web.ViewModels.ShoppingCart;
    using Microsoft.EntityFrameworkCore;
    using System.Security.Claims;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    public class ShoppingCartController : Controller
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDistributedCache distributedCache;

        public ShoppingCartController(
            IDeletableEntityRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager,
            IShoppingCartService shoppingCartService,
            IHttpContextAccessor httpContextAccessor,
            IDistributedCache distributedCache)
        {
            this.productRepository = productRepository;
            this.userManager = userManager;
            this.shoppingCartService = shoppingCartService;
            this.httpContextAccessor = httpContextAccessor;
            this.distributedCache = distributedCache;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(this.HttpContext.User);
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
            var user = await userManager.GetUserAsync(this.HttpContext.User);
            await this.shoppingCartService.AddToCartAsync(id, user.Id, quantity);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var user = await userManager.GetUserAsync(this.HttpContext.User);
            await this.shoppingCartService.RemoveFromCartAsync(id, user.Id);

            return this.RedirectToAction("Index");
        }
    }
}
