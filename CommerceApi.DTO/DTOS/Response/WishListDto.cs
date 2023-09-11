using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CommerceApi.DTO.DTOS
{
    public class WishListDto
    {
        [JsonPropertyName("wishlist_id")]
        public string UID { get; set; }

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }

        [JsonPropertyName("total_unique_items")]
        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        [JsonPropertyName("cart_products")]
        public ICollection<CartProductDto> CartProducts { get; set; }


    }
}
