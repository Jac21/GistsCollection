using System;
using System.ComponentModel.DataAnnotations;

namespace APM.WebAPI.Models
{
    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is reqiured!", AllowEmptyStrings = false)]
        [MinLength(4, ErrorMessage = "Product Name minimum length is 5 characters!")]
        [MaxLength(100, ErrorMessage = "Product Name maximum length is 100 characters!")]
        public string ProductName { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}