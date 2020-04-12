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

        //public ShoppingCart()
        //{
        //    ShoppingCartItems = new List<ShoppingCartItem>();
        //}

        public ShoppingCart()
        {

        }

        //public ShoppingCart(IRepository<ShoppingCartItem> shoppingCartItemRepository)
        //{
        //    this.shoppingCartItemRepository = shoppingCartItemRepository;
        //}

        public string UserId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        //public List<ShoppingCartItem> GetShoppingCartItems()
        //{
        //    return ShoppingCartItems ??
        //        (ShoppingCartItems =
        //        shoppingCartItemRepository.All().Where(c => c.ShoppingCartId == Id).Include(s => s.Product).ToList());
        //}


    }
}
