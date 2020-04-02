namespace PointNet.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PointNet.Data.Common.Models;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            var products = new List<Product>
            {
                new Product() { Title = "Xiaomi Redmi Note 7", CategoryId = 1, Description = "64GB Internal memory , 4GB RAM, Two Cameras, 6.1 inch display, ...", Price = 310, ImageUrl = "https://fdn2.gsmarena.com/vv/bigpic/xiaomi-redmi-note-7-pro.jpg" },
                new Product() { Title = "IPhone X", CategoryId = 1, Description = "999GB Internal Memory, 99GB RAM, 999 Cameras, 99 inch display", Price = 9999, ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/refurb-iphoneX-silver_AV2?wid=1144&hei=1144&fmt=jpeg&qlt=80&op_usm=0.5,0.5&.v=1548459943718" },
                new Product() { Title = "Samsung Galaxy", CategoryId = 1, Description = "Vsichki Ekstri", Price = 1000, ImageUrl = "https://www.samsung.com/global/galaxy/main/images/kv_galaxy-s20_s.jpg" },
                new Product() { Title = "HP-ProBook", CategoryId = 3, Description = "LAPTOP", Price = 1100, ImageUrl = "https://id-media.apjonlinecdn.com/catalog/product/cache/b3b166914d87ce343d4dc5ec5117b502/3/Z/3ZS27PA-1_T1539255387.png" },
            };
            foreach (var product in products)
            {
                await dbContext.Products.AddAsync(new Product
                {
                    Title = product.Title,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                });
            }
        }
    }
}
