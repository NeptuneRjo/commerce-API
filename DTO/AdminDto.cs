using CommerceClone.Models;
using System.Text.Json.Serialization;

namespace CommerceClone.DTO
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }

        [JsonPropertyName("secret_key")]
        public string SecretKey { get; set; }

        public ICollection<StoreDto> Stores { get; set; }
    }
}
