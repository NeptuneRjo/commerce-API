using CommerceClone.Models;
using System.Text.Json.Serialization;

namespace CommerceClone.DTO
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("admin_id")]
        public int AdminId { get; set; }
    }
}
