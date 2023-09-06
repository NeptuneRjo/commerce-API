using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class CartDto
    {
        public int Id { get; set; }
        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }
        [JsonPropertyName("total_unique_items")]
        public int TotalUniqueItems { get; set; }
        public string Subtotal { get; set; }

        [JsonPropertyName("cart_items")]
        public ICollection<CartItemDto> CartItems { get; set; }
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }

        public CartDto()
        {
            CartItems = new List<CartItemDto>();

            TotalItems = CartItems.Count();

            TotalUniqueItems = CartItems.GroupBy(e => e.ItemId)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .ToList()
                .Count();

            Subtotal = CartItems.Sum(e => e.Total).ToString();
        }
    }
}
