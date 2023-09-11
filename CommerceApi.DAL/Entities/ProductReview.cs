using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class ProductReview
    {
        public int ProductId { get; set; }

        public int ReviewId { get; set; }

        public Review Review { get; set; }

        public Product Product { get; set; }
    }
}
