namespace PointNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Services.Mapping;
    using PointNet.Web.ViewModels.Catalog;

    public class ProductsService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsService(
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Comment> commentsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.productsRepository = productRepository;
            this.commentsRepository = commentsRepository;
            this.userManager = userManager;
        }

        public async Task AddNewProductAsync<T>(ProductViewModel viewModel)
        {
            Product product = new Product()
            {
                Title = viewModel.Title,
                Description = viewModel.Description,
                Price = viewModel.Price,
                Quantity = viewModel.Quantity,
                ImageUrl = viewModel.ImageUrl,
                CategoryId = viewModel.CategoryId,
                ImageName = viewModel.ImageName
            };
            await this.productsRepository.AddAsync(product);

            await this.productsRepository.SaveChangesAsync();

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
            ICollection<Comment> productComments = this.commentsRepository.All().Where(x => x.ProductId == id).ToList();

            var product = this.productsRepository.FindById(id);

            var model = new ProductViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                ImageName = product.ImageName,
                CategoryId = product.CategoryId,
                Comments = productComments,
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

        public async Task RemoveProductAsync(int productId)
        {
            if (productId != null)
            {
                var productToDelete = this.productsRepository.FindById(productId);

                this.productsRepository.Delete(productToDelete);

                await this.productsRepository.SaveChangesAsync();
            }

            return;
        }

        public async Task AddCommentAsync(int productId, string commentContent, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            var newComment = new Comment
            {
                UserId = userId,
                Content = commentContent,
                ProductId = productId,
                UserName = user.NormalizedUserName,
            };

            await this.commentsRepository.AddAsync(newComment);

            await this.commentsRepository.SaveChangesAsync();

            return;
        }
    }
}
