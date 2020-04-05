using PointNet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointNet.Data.Models
{
    public class ShoppingCartItem : BaseDeletableModel<int>
    {
        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        public int ShoppingCartId { get; set; }
    }
}
