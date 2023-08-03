using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Count { get; set; }

        public Store? Store { get; set; }
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }
    }

    public class ItemModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[]? Image { get; set; }
        public int Count { get; set; } = 1;
    }

    public class ItemModelUpdate 
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int? Count { get; set; }
    }
}
