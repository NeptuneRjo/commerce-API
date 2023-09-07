using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class ProductReview
    {
        public string ProductId { get; set; }

        public string ReviewId { get; set; }

        public Review Review { get; set; }

        public Product Product { get; set; }
    }
}
