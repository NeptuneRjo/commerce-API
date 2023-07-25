namespace CommerceClone.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        public int ItemId { get; set; }

        public Cart Cart { get; set; }
        public Item Item { get; set; }

        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public CartItem()
        {
            Total = Item.Price * Quantity;
        }
    }
}
