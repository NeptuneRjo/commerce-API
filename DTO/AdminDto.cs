using CommerceClone.Models;

namespace CommerceClone.DTO
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PublicKey { get; set; }
        public string SecretKey { get; set; }
        public ICollection<Store> Stores { get; set; }
    }
}
