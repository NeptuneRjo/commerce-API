using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required, MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public string Reviewer { get; set; }

        public ICollection<ProductReview> ProductReviews { get; set; }
    }
}
