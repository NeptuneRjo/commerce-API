namespace CommerceClone.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        public Cart Cart { get; set; }
        public int CartId { get; set; }
    }
}
