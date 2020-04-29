namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<Product> productRepository;
        private readonly IDistributedCache distributedCache;

        public ShoppingCartService(
            IRepository<Product> productRepository,
            IDistributedCache distributedCache)
        {
            this.productRepository = productRepository;
            this.distributedCache = distributedCache;
        }

        public List<ShoppingCartItem> GetCart(string userId)
        {
            List<ShoppingCartItem> deserializedProductList;
            var cachedShoppingCartItems = this.distributedCache.GetString(userId);

            if (cachedShoppingCartItems != null)
            {
                deserializedProductList = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(cachedShoppingCartItems);

                return deserializedProductList;
            }
            else
            {
                return null;
            }
        }

        public async Task AddToCartAsync(int productId, string userId, int quantity)
        {
            List<ShoppingCartItem> deserializedCartItems;

            var productToAdd = this.productRepository.FindById(productId);

            if (productToAdd != null)
            {
                var productsInUserCart = this.distributedCache.GetString(userId);

                if ((productToAdd.Quantity - quantity) < 0)
                {
                    return;
                }
                else
                {
                    productToAdd.Quantity = productToAdd.Quantity - quantity;
                    await this.productRepository.SaveChangesAsync();
                }

                var cartItem = new ShoppingCartItem
                {
                    Id = productToAdd.Id,
                    Title = productToAdd.Title,
                    ImageUrl = productToAdd.ImageUrl,
                    Price = productToAdd.Price,
                    Amount = quantity,
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

        public async Task RemoveFromCartAsync(int productId, string userId)
        {
            var productsInUserCart = this.distributedCache.GetString(userId);
            var deserializedCartItems = JsonConvert.DeserializeObject<List<ShoppingCartItem>>(productsInUserCart);
            var itemToRemove = deserializedCartItems.FirstOrDefault(x => x.Id == productId);

            if (itemToRemove.Amount > 1)
            {
                itemToRemove.Amount--;
                this.productRepository.FindById(productId).Quantity++;
            }
            else
            {
                deserializedCartItems.Remove(itemToRemove);
                this.productRepository.FindById(productId).Quantity++;
            }

            await this.productRepository.SaveChangesAsync();

            var serializeCartItems = JsonConvert.SerializeObject(deserializedCartItems);

            this.distributedCache.SetString(userId, serializeCartItems, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            });

            return;
        }
    }
}
