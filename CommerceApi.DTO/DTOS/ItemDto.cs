using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public int Count { get; set; }
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }
    }
}
