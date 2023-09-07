namespace CommerceApi.DAL.Entities
{
    public class WishListProduct
    {
        public string WishListId { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        public WishList WishList { get; set; }

        public int WishListProductQuantity { get; set; }

        public decimal WishListProductTotal { get; set; }
    }
}
