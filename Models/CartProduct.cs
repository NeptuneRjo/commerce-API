﻿namespace CommerceClone.Models
{
    public class CartProduct
    {
        public int CartId { get; set; }
        
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Cart Cart { get; set; }
    }
}
