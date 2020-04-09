namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository;
        private readonly IServiceProvider services;
        private readonly IRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ShoppingCartService(
            IServiceProvider services,
            IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository,
            IRepository<Product> productRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.shoppingCartItemRepository = shoppingCartItemRepository;
            this.services = services;
            this.productRepository = productRepository;
            this.userManager = userManager;
        }

        public void AddToCart(int productId, ApplicationUser user)
        {
            var productToAdd = this.productRepository.FindById(productId);

            bool productExistsInCart = false;

            if (user.ShoppingCart.ShoppingCartItems.Count > 0)
            {
                productExistsInCart = user.ShoppingCart.ShoppingCartItems.Exists(x => x.Id == productId);
            }

            if (!productExistsInCart)
            {
                var cartItem = new ShoppingCartItem
                {
                    Product = productToAdd,
                    Amount = 1,
                    ShoppingCartId = user.ShoppingCartId,
                };
                user.ShoppingCart.ShoppingCartItems.Add(cartItem);
                this.shoppingCartItemRepository.AddAsync(cartItem);
                this.shoppingCartItemRepository.SaveChangesAsync();
                return;
            }
            else
            {
                var productInCart = user.ShoppingCart.ShoppingCartItems.Find(x => x.Id == productId);
                productInCart.Amount++;
            }
        }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetCart(ApplicationUser user)
        {
            var cartProducts = user.ShoppingCart.ShoppingCartItems;

            return cartProducts;
        }

        public decimal GetShoppingCartTotal()
        {
            throw new NotImplementedException();
        }

        public int RemoveItemFromCart(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
