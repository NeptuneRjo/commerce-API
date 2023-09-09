using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class ProductDto
    {
        [JsonPropertyName("product_id")]
        public string ProductId { get; set; }

        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public string Description { get; set; }
        
        //public byte[]? Image { get; set; }
        
        public int Count { get; set; }

        public string SKU { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        [JsonPropertyName("quantity")]
        public int StockQuantity { get; set; }

        [JsonPropertyName("in_stock")]
        public bool InStock { get; set; }

        public string Currency { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("reviews")]
        public ICollection<ProductReviewDto> ProductReviews { get; set; }

    }
}
