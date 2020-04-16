namespace PointNet.Services.Data.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;

    public interface IShoppingCartService
    {
        public List<ShoppingCartItem> GetCart(string userId);

        public void AddToCart(int productId, string userId);

        public void RemoveFromCart(int productId, string userId);

        //public static ShoppingCart GetCart(IServiceProvider service);

        //public List<ShoppingCartItem> GetShoppingCartItems();

        //public void AddToCart(Product product, int amount);

        //public decimal GetShoppingCartTotal();

        //public int RemoveItemFromCart(Product product);

        //public void ClearCart();
    }
}
