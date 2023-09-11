using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class ReviewDto
    {
        [JsonPropertyName("review_id")]
        public string UID { get; set; }

        public string Content { get; set; }

        public UserDto Reviewer { get; set; }
    }
}
