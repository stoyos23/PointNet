using PointNet.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointNet.Data.Models
{
    public class Order : BaseModel<int>
    {
        public int UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Address ShippingAddress { get; set; }

        public decimal Price { get; set; }

        public bool IsShipped { get; set; }

    }
}
