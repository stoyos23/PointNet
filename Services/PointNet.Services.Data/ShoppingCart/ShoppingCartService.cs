using Microsoft.AspNetCore.Http;
using PointNet.Data.Common.Models;
using PointNet.Data.Common.Repositories;
using PointNet.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointNet.Services.Data.ShoppingCart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository;
        private readonly IServiceProvider service;

        public ShoppingCartService(IServiceProvider service, IDeletableEntityRepository<ShoppingCartItem> shoppingCartItemRepository)
        {
            this.shoppingCartItemRepository = shoppingCartItemRepository;
            this.service = service;
        }

        //Methods
        #region
        public void AddToCart()
        {
            throw new NotImplementedException();
        }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }

        public PointNet.Data.Models.ShoppingCart GetCart()
        {
            throw new NotImplementedException();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            throw new NotImplementedException();
        }

        public decimal GetShoppingCartTotal()
        {
            throw new NotImplementedException();
        }

        public int RemoveItemFromCart(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
