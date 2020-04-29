namespace PointNet.Data.Models
{
    using PointNet.Data.Common.Models;

    public class ShoppingCartItem : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}
