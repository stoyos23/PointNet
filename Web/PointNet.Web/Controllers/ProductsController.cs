namespace PointNet.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Services.Data;
    using PointNet.Web.ViewModels.Catalog;
    using ReflectionIT.Mvc.Paging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsController : Controller
    {
        private readonly IProductsService productService;
        private readonly IDeletableEntityRepository<Product> productRepository;

        public ProductsController(IProductsService productService, IDeletableEntityRepository<Product> productRepository)
        {
            this.productService = productService;
            this.productRepository = productRepository;
        }

        // returns all products from from one category
        public async Task<IActionResult> Index(int categoryId, int page = 1)
        {
            var products = this.productService.GetProductsInSpecificCategory<ProductViewModel>(categoryId);

            return this.View(await PaginatedList<ProductViewModel>.CreateAsync(products, page , 6));
        }


        public IActionResult ProductDetails(int id)
        {
            var productDetails = this.productService.GetProductDetails(id);
            return this.View(productDetails);
        }
    }
}
