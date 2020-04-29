namespace PointNet.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using PointNet.Web.ViewModels.Catalog;

    public interface IProductsService
    {
        // public AddNewProduct(string title, string description, string imageUrl, decimal price, int categoryId);
        IQueryable<T> GetProductsInSpecificCategory<T>(int? id);

        public Task AddNewProductAsync<T>(ProductViewModel viewModel);

        public ProductViewModel GetProductDetails(int id);

        public IQueryable<T> GetProductsByName<T>(string productName);

        public Task RemoveProductAsync(int productId);

        public Task AddCommentAsync(int productId, string commentContent, string userId);
    }
}
