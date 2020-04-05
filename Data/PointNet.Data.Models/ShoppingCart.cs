using PointNet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointNet.Data.Models
{
    public class ShoppingCart : BaseDeletableModel<int>
    {
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
