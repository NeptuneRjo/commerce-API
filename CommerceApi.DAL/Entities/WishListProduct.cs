namespace CommerceApi.DAL.Entities
{
    public class WishListProduct
    {
        public int WishListId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public WishList WishList { get; set; }
    }
}
