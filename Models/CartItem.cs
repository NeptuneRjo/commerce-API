using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }

        [JsonPropertyName("cart_id")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
