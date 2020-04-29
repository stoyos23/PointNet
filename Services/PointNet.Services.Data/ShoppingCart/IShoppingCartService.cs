namespace PointNet.Services.Data.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PointNet.Data.Models;

    public interface IShoppingCartService
    {
        public List<ShoppingCartItem> GetCart(string userId);

        public Task AddToCartAsync(int productId, string userId, int quantity);

        public Task RemoveFromCartAsync(int productId, string userId);
    }
}
