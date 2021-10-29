using System;
using System.Collections.Generic;

namespace Shops.Services
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);

        public Client AddClient(string name, int money);

        public Product RegisterProduct(string name);

        public Product GetProduct(Guid id);

        public Product FindProduct(string name);

        public Client GetClient(Guid id);

        public Shop GetShop(Guid id);

        public Shop FindShop(string address);

        public void SetPrice(Shop shop, Product product, uint newPrice);

        public void Buy(Shop shop, Client buyer, List<Lot> lots);

        public void Deliver(Shop shop, List<Lot> lots);

        public Shop FindCheapestShop(List<Lot> lots);
    }
}