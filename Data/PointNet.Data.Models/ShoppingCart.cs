namespace PointNet.Data.Models
{
    using System.Collections.Generic;

    using PointNet.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
        }

        public string UserId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
