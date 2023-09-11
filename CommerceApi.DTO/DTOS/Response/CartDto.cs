using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class CartDto
    {
        [JsonPropertyName("cart_id")]
        public string UID { get; set; }

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }

        [JsonPropertyName("total_unique_items")]
        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        [JsonPropertyName("cart_products")]
        public ICollection<CartProductDto> CartProducts { get; set; }
    }
}
