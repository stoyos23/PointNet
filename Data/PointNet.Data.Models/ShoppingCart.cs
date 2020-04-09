namespace PointNet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;

    public class ShoppingCart : BaseDeletableModel<int>
    {
        public ShoppingCart()
        {
            ShoppingCartItems = new List<ShoppingCartItem>();
        }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
