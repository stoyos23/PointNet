namespace PointNet.Data.Common.Models
{
    using PointNet.Data.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
