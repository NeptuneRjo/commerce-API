﻿namespace CommerceClone.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
