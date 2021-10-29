using System;

namespace Shops
{
    public class Product
    {
        public Product(string name, Guid id)
        {
            Name = name;
            ID = id;
        }

        public string Name { get; }

        public Guid ID { get; }
    }
}