using PointNet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointNet.Data.Models
{
    public class ShoppingCartItem : BaseDeletableModel<int>
    {

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}
