
namespace CommerceApi.DTO.DTOS
{
    public class CartProductToAddDto
    {
        public string CartId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
