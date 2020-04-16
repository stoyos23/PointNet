namespace PointNet.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PointNet.Data.Common.Models;
    using PointNet.Web.ViewModels.Catalog;

    public interface IProductsService
    {
        // public AddNewProduct(string title, string description, string imageUrl, decimal price, int categoryId);
        IQueryable<T> GetProductsInSpecificCategory<T>(int? id);

        public void AddNewProduct<T>(ProductViewModel viewModel);

        public ProductViewModel GetProductDetails(int id);
    }
}
