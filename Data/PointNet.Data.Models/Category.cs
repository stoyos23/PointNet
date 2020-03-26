namespace PointNet.Data.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public string ImageuUrl { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
