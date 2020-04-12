namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository;
        private readonly IRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDeletableEntityRepository<ShoppingCart> shoppingCartRepository;
        private readonly IDistributedCache distributedCache;
        private readonly ShoppingCart shoppingCart;

        public ShoppingCartService(
            IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository,
            IRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IDeletableEntityRepository<ShoppingCart> shoppingCartRepository,
            IDistributedCache distributedCache
            )
        {
            this.shoppingCartItemRepository = shoppingCartItemRepository;
            this.productRepository = productRepository;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.shoppingCartRepository = shoppingCartRepository;
            this.distributedCache = distributedCache;
        }

        public List<ShoppingCartItem> GetCart(string userId)
        {
            var productRevert = this.distributedCache.GetString(userId);
            var deserialize = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(productRevert);

            return deserialize;
        }

        public void AddToCart(int productId, string userId)
        {
            List<ShoppingCartItem> deserializedCartItems;

            var productToAdd = this.productRepository.FindById(productId);

            var productsInUserCart = this.distributedCache.GetString(userId);

            var cartItem = new ShoppingCartItem
            {
                Id = productToAdd.Id,
                Title = productToAdd.Title,
                ImageUrl = productToAdd.ImageUrl,
                Price = productToAdd.Price,
                Amount = 1,
            };

            if (productsInUserCart != null)
            {
                deserializedCartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(productsInUserCart);

                var findProductExist = deserializedCartItems.Find(x => x.Id == productToAdd.Id);

                if (findProductExist != null)
                {
                    findProductExist.Amount++;
                }
                else
                {
                    deserializedCartItems.Add(cartItem);
                }
            }
            else
            {
                var productsList = new List<ShoppingCartItem> { cartItem };
                var serializedProductList = JsonConvert.SerializeObject(productsList);

                this.distributedCache.SetString(userId, serializedProductList, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
                });
                return;
            }

            var serializedCartItems = JsonConvert.SerializeObject(deserializedCartItems);
            this.distributedCache.SetString(userId, serializedCartItems, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            });
        }
    }
}
