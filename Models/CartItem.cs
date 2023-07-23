using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class CartItem
    {
        [Key]
        public string Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public Cart Cart { get; set; }
        public string CartId { get; set; }

        public CartItem()
        {
            Total = Item.Price * Quantity;
        }
    }
}
