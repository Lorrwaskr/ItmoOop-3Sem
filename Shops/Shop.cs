using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops
{
    public class Shop
    {
        public Shop(string name, string address, Guid id)
        {
            Name = name;
            Address = address;
            ID = id;
            Lots = new List<Lot>();
        }

        private Shop(string name, string address, Guid id, List<Lot> lots)
        {
            Name = name;
            Address = address;
            ID = id;
            Lots = lots;
        }

        public string Name { get; }

        public string Address { get; }

        public Guid ID { get; }

        public List<Lot> Lots { get; }

        public bool IsContainsLots(List<Lot> lots)
        {
            return lots.All(lot => Lots.Find(lot1 => lot1.Product.ID == lot.Product.ID) != null);
        }

        public Lot GetLot(Guid productId)
        {
            return Lots.Find(lot => lot.Product.ID == productId);
        }

        public ShopBuilder ToBuilder()
        {
            return new ShopBuilder(this);
        }

        public class ShopBuilder
        {
            private string _name;
            private string _address;
            private Guid _id;
            private List<Lot> _lots;

            public ShopBuilder(Shop shop)
            {
                _name = shop.Name;
                _address = shop.Address;
                _id = shop.ID;
                _lots = shop.Lots;
            }

            public ShopBuilder WithLots(List<Lot> lots)
            {
                _lots = lots;
                return this;
            }

            public ShopBuilder AddLot(Lot lot)
            {
                _lots.Add(lot);
                return this;
            }

            public Shop Build()
            {
                return new Shop(_name, _address, _id, _lots);
            }
        }
    }
}
