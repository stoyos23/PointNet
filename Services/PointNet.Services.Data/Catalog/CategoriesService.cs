namespace PointNet.Services.Data.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;
    using PointNet.Services.Mapping;
    using PointNet.Web.ViewModels.Catalog;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public CatalogViewModel GetAllCategories()
        {
            var allCategories = this.categoriesRepository.All().To<CategoriesViewModel>();

            var model = new CatalogViewModel
            {
                Categories = allCategories.ToList()
            };

            return model;
        }

        public IEnumerable<Category> AllCategoriesList()
        {
            var allCategories = this.categoriesRepository.All();

            return allCategories;
        }
    }
}
