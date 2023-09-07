using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class WishListProductDto
    {
        public ProductDto Product { get; set; }

        [JsonPropertyName("quantity")]
        public int WishListProductQuantity { get; set; }

        [JsonPropertyName("total")]
        public decimal WishListProductTotal { get; set; }
    }
}
