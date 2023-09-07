using System.Text.Json.Serialization;

namespace CommerceApi.DTO.DTOS
{
    public class ReviewDto
    {
        [JsonPropertyName("review_id")]
        public int ReviewId { get; set; }

        public string Content { get; set; }

        public UserDto Reviewer { get; set; }
    }
}
