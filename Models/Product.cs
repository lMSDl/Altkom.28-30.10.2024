﻿namespace Models
{
    public class Product : Entity
    {
        public string Name { get; set; } = string.Empty;
        public float Price { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}