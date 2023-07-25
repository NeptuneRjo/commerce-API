using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonPropertyName("secret_key")]
        public string SecretKey { get; set; }
        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; }

        [JsonPropertyName("stores")]
        public ICollection<Store>? Stores{ get; set; }

        public Admin()
        {
            Stores = new List<Store>();
            PublicKey = "";
            SecretKey = "";
        }
    }
}
