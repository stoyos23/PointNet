namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;
    using PointNet.Data.Models;

    public interface IShoppingCartService
    {
        public void AddToCart(int productId, ApplicationUser user);

        public List<ShoppingCartItem> GetCart(ApplicationUser user);

        public decimal GetShoppingCartTotal();

        public int RemoveItemFromCart(Product product);

        public void ClearCart();
    }
}
