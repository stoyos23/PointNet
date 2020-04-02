namespace PointNet.Services.Data.Catalog
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PointNet.Data.Common.Models;
    using PointNet.Data.Common.Repositories;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Category> AllCategories()
        {
            var allCategories = this.categoriesRepository.All();

            return allCategories;
        }
    }
}
