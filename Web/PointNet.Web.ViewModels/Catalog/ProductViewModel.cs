﻿namespace PointNet.Web.ViewModels.Catalog
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using PointNet.Data.Common.Models;
    using PointNet.Services.Mapping;

    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "length between 2 and 50", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string ImageName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity can not be negative or 0")]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, int.MaxValue, ErrorMessage = "Price can not be negative or 0")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public List<SelectListItem> AllCategories { get; set; }

        public string CommentToAdd { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
