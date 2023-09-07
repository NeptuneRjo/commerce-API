namespace CommerceApi.DAL.Entities
{
    public class CartProduct
    {
        public string CartId { get; set; }
        
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public Cart Cart { get; set; }

        public int CartProductQuantity { get; set; }

        public decimal CartProductTotal { get; set; }
    }
}
