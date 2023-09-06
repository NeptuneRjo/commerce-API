using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MaxLength(65)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public byte[] Image { get; set; }

        public string SKU { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }

        [Required, MaxLength(65)]
        public string Brand { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public bool InStock { get; set; }

        public string Currency { get; set; }

        public List<ProductReview> ProductReviews { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
