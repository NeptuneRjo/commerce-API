using CommerceClone.Models;
using System.Text.Json.Serialization;

namespace CommerceClone.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }

        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }

        [JsonPropertyName("cart_id")]
        public int CartId { get; set; }

        public int Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
