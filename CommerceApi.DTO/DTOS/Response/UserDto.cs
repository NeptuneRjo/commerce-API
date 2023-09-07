using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class UserDto
    {
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }

        public string Email { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        public CartDto Cart { get; set; }

        [JsonPropertyName("is_admin")]
        public bool IsAdmin { get; set; }
    }
}
