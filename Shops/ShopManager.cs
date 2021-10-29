using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops
{
    public class ShopManager : Services.IShopManager
    {
        private List<Shop> shops;
        private List<Product> products;
        private List<Client> clients;

        public ShopManager()
        {
            shops = new List<Shop>();
            products = new List<Product>();
            clients = new List<Client>();
        }

        public Shop AddShop(string name, string address)
        {
            var newShop = new Shop(name, address, IdGenerator.MakeID());
            shops.Add(newShop);
            return newShop;
        }

        public Client AddClient(string name, int money)
        {
            if (money < 0)
                throw new ArgumentException("Money must be > 0");
            var newClient = new Client(name, (uint)money, IdGenerator.MakeID());
            clients.Add(newClient);
            return newClient;
        }

        public Product RegisterProduct(string name)
        {
            if (products.Any(product => product.Name == name))
            {
                throw new ArgumentException("Product already exist");
            }

            var newProduct = new Product(name, IdGenerator.MakeID());
            products.Add(newProduct);
            return newProduct;
        }

        public Product GetProduct(Guid id)
        {
            return products.Find(product => product.ID == id);
        }

        public Product FindProduct(string name)
        {
            return products.Find(product => product.Name == name);
        }

        public Client GetClient(Guid id)
        {
            return clients.Find(client => client.ID == id);
        }

        public Shop GetShop(Guid id)
        {
            return shops.Find(shop => shop.ID == id);
        }

        public Shop FindShop(string address)
        {
            return shops.Find(shop => shop.Address == address);
        }

        public void SetPrice(Shop shop, Product product, uint newPrice)
        {
            var newLots = new List<Lot>(shop.Lots);
            Lot oldLot = newLots.Find(lot => lot.Product.ID == product.ID) ?? new Lot(product, 0, 0);
            Lot newLot = oldLot.ToBuilder().WithPrice(newPrice).Build();
            newLots.Remove(oldLot);
            newLots.Add(newLot);
            Shop newShop = shop.ToBuilder().WithLots(newLots).Build();
            shops.Remove(shop);
            shops.Add(newShop);
        }

        public void Buy(Shop shop, Client buyer, List<Lot> lots)
        {
            if (!buyer.IsCanBuy(lots))
                throw new ArgumentException(buyer.Name + " don't have enough money");
            if (!shop.IsContainsLots(lots))
                throw new Exception("Shop doesn't contain some lots");
            var newLots = new List<Lot>(shop.Lots);
            uint money = buyer.Money;
            foreach (Lot lot in lots)
            {
                Lot oldLot = newLots.Find(lot1 => lot1.Product.ID == lot.Product.ID);
                if (oldLot.Amount < lot.Amount)
                    throw new Exception("Shop doesn't contain enough of " + lot.Product.Name);
                Lot newLot = oldLot.ToBuilder().RemoveAmount(lot.Amount).Build();
                money -= lot.Amount * oldLot.Price;
                newLots.Remove(oldLot);
                newLots.Add(newLot);
            }

            Shop newShop = shop.ToBuilder().WithLots(newLots).Build();
            shops.Remove(shop);
            shops.Add(newShop);
            Client newBuyer = buyer.ToBuilder().WithMoney(money).Build();
            clients.Remove(buyer);
            clients.Add(newBuyer);
        }

        public void Deliver(Shop shop, List<Lot> lots)
        {
            var newLots = new List<Lot>(shop.Lots);
            foreach (Lot lot in lots)
            {
                Lot oldLot = newLots.Find(lot1 => lot1.Product.ID == lot.Product.ID);
                if (oldLot != null)
                {
                    Lot newLot = oldLot.ToBuilder().AddAmount(lot.Amount).WithPrice(lot.Price).Build();
                    newLots.Remove(oldLot);
                    newLots.Add(newLot);
                }
                else
                {
                    newLots.Add(lot);
                }
            }

            Shop newShop = shop.ToBuilder().WithLots(newLots).Build();
            shops.Add(newShop);
            shops.Remove(shop);
        }

        public Shop FindCheapestShop(List<Lot> lots)
        {
            Shop result = null;
            long minPrice = long.MaxValue;

            foreach (Shop shop in shops)
            {
                if (!shop.IsContainsLots(lots))
                    continue;
                long sum = 0;
                foreach (Lot lot in lots)
                {
                    Lot oldLot = shop.Lots.Find(lot1 => lot1.Product.ID == lot.Product.ID);
                    if (oldLot.Amount < lot.Amount)
                    {
                        sum = -1;
                        break;
                    }

                    sum += lot.Amount * oldLot.Price;
                }

                if (sum != -1 && sum < minPrice)
                {
                    minPrice = sum;
                    result = shop;
                }
            }

            return result;
        }
    }
}
