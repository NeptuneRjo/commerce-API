using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class CartProductDto
    {
        public ProductDto Product { get; set; }

        [JsonPropertyName("quantity")]
        public int CartProductQuantity { get; set; }

        [JsonPropertyName("total")]
        public decimal CartProductTotal { get; set; }
    }
}
