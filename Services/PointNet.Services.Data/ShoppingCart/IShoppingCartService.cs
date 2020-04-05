namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Models;

    public interface IShoppingCartService
    {
        public ShoppingCart GetCart();

        public void AddToCart();

        public List<ShoppingCartItem> GetShoppingCartItems();

        public decimal GetShoppingCartTotal();

        public int RemoveItemFromCart(Product product);

        public void ClearCart();

    }
}
