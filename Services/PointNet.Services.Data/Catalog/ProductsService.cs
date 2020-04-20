namespace PointNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Services.Mapping;
    using PointNet.Web.ViewModels.Catalog;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;

        public ProductsService(IDeletableEntityRepository<Product> productRepository)
        {
            this.productsRepository = productRepository;
        }

        public void AddNewProduct<T>(ProductViewModel viewModel)
        {
            Product product = new Product()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                ImageUrl = viewModel.ImageUrl,
                CategoryId = viewModel.CategoryId,
            };
            this.productsRepository.AddAsync(product);
            this.productsRepository.SaveChangesAsync();
            return;
        }

        /// <summary>
        /// Returns products from Specific category.(If CategoryId == null returns all products).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public IQueryable<T> GetProductsInSpecificCategory<T>(int? id = null)
        {
            // this.productsRepository.All().Where(x => x.CategoryId == id);

            // var products = this.productsRepository.All().Where(x => x.CategoryId == id).Select(x => new ProductViewModel
            // {
            //    Title = x.Title,
            //    Description = x.Description,
            //    ImageUrl = x.ImageUrl,
            //    Price = x.Price,
            // }).ToList();


            var products = this.productsRepository.All();

            if (id.HasValue)
            {
                products = this.productsRepository.All().Where(x => x.CategoryId == id);
            }

            return products.To<T>();
        }

        public ProductViewModel GetProductDetails(int id)
        {
            var product = this.productsRepository.FindById(id);
            var model = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
            };

            return model;
        }
    }
}
