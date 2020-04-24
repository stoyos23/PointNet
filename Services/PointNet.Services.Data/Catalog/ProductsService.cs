﻿namespace PointNet.Services.Data
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

        public IQueryable<T> GetProductsInSpecificCategory<T>(int? id = null)
        {
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

        public IQueryable<T> GetProductsByName<T>(string productName)
        {
            IQueryable<Product> products = null;

            if (productName != null)
            {
                products = this.productsRepository.All().Where(x => x.Title.Contains(productName));

                return products.To<T>();
            }

            return this.productsRepository.All().To<T>();
        }

        public void RemoveProduct(int productId)
        {
            if (productId != null)
            {
                var productToDelete = this.productsRepository.FindById(productId);
                this.productsRepository.Delete(productToDelete);
                this.productsRepository.SaveChangesAsync();
            }

            return;
        }
    }
}
