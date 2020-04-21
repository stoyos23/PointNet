namespace PointNet.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;

    public class ShoppingCart : BaseDeletableModel<int>
    {

        public ShoppingCart()
        {

        }

        public string UserId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
