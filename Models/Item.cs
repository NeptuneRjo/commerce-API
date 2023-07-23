using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class Item
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Count { get; set; }

        public Store Store { get; set; }
        public string StoreId { get; set; }

        public CartItem CartItem { get; set; }
        public string CartItemId { get; set; }
    }
}
