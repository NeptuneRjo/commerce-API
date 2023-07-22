namespace CommerceClone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Cart Cart { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

    }
}
