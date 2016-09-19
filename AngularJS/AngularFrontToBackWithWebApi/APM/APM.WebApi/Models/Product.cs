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
        public string ProductName { get; set; }
        public DateTime ReleaseDate { get; set; }

    }
}