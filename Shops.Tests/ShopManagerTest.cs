using System.Collections.Generic;
using NUnit.Framework;
using Shops.Services;

namespace Shops.Tests
{
    public class ShopManagerTest
    {
        
        private IShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void DeliverGoodsToStore_ShopContainsGoods()
        {
            Shop shop = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            Client client = _shopManager.AddClient("Nikita", 10000);
            
            var list = new List<Lot>
            {
                new Lot(orange, 65, 100),
                new Lot(banana, 70, 250)
            };
            _shopManager.Deliver(_shopManager.GetShop(shop.ID), list);
            
            Assert.That(_shopManager.GetShop(shop.ID).IsContainsLots(list));
        }

        [Test]
        public void SetPrice_ThePriceIsSet()
        {
            Shop shop = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");

            const int expectedPrice = 80;
            _shopManager.SetPrice(_shopManager.GetShop(shop.ID), orange, expectedPrice);
            Assert.AreEqual(expectedPrice,_shopManager.GetShop(shop.ID).GetLot(orange.ID).Price);
        }
        
        [Test]
        public void DeliverGoodsAndSetPrice_PriceChanged()
        {
            Shop shop = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");

            var list = new List<Lot>
            {
                new Lot(orange, 65, 100),
            };
            _shopManager.Deliver(_shopManager.GetShop(shop.ID), list);
            
            const int expectedPrice = 80;
            _shopManager.SetPrice(_shopManager.GetShop(shop.ID), orange, expectedPrice);
            Assert.AreEqual(expectedPrice,_shopManager.GetShop(shop.ID).GetLot(orange.ID).Price);
        }
        
        [Test]
        public void BuyGoods_ShopChangedAndClientMoneyChanged()
        {
            Shop shop = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            Client client = _shopManager.AddClient("Nikita", 10000);
            
            var list = new List<Lot>
            {
                new Lot(orange, 65, 100),
                new Lot(banana, 70, 250)
            };
            _shopManager.Deliver(_shopManager.GetShop(shop.ID), list);

            list.Clear();
            list.Add(new Lot(orange, 0, 50));
            list.Add(new Lot(banana, 0, 51));
            _shopManager.Buy(_shopManager.GetShop(shop.ID), client, list);

            const int expectedMoney = 10000 - (50 * 65 + 51 * 70);
            Assert.AreEqual(50, _shopManager.GetShop(shop.ID).GetLot(orange.ID).Amount);
            Assert.AreEqual(199, _shopManager.GetShop(shop.ID).GetLot(banana.ID).Amount);
            Assert.AreEqual(expectedMoney, _shopManager.GetClient(client.ID).Money);
        }

        [Test]
        public void FindCheapestShop()
        {
            Shop shop1 = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            var list = new List<Lot>
            {
                new Lot(orange, 65, 100),
                new Lot(banana, 70, 250)
            };

            _shopManager.Deliver(shop1, list);

            Shop shop2 = _shopManager.AddShop("Renat's", "Yanino");

            list.Clear();
            list.Add(new Lot(orange, 60, 200));
            list.Add(new Lot(banana, 70, 150));

            _shopManager.Deliver(shop2, list);

            list.Clear();
            list.Add(new Lot(orange, 0, 50));
            list.Add(new Lot(banana, 0, 51));
            
            Shop cheapest = _shopManager.FindCheapestShop(list);
            Assert.AreEqual(shop2.ID, cheapest.ID);
        }

        [Test]
        public void TryFindCheapestShop_NotEnoughGoods_CheapestIsNull()
        {
            Shop shop1 = _shopManager.AddShop("Shtyb's", "Avtovo");

            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            var list = new List<Lot>
            {
                new Lot(orange, 65, 25),
                new Lot(banana, 70, 30)
            };

            _shopManager.Deliver(shop1, list);

            Shop shop2 = _shopManager.AddShop("Renat's", "Yanino");

            list.Clear();
            list.Add(new Lot(orange, 60, 40));
            list.Add(new Lot(banana, 70, 50));

            _shopManager.Deliver(shop2, list);

            list.Clear();
            list.Add(new Lot(orange, 0, 50));
            list.Add(new Lot(banana, 0, 51));
            
            Shop cheapest = _shopManager.FindCheapestShop(list);
            Assert.AreEqual(null, cheapest);
        }
        
        [Test]
        public void TryFindCheapestShop_NoGoods_CheapestIsNull()
        {
            Shop shop1 = _shopManager.AddShop("Shtyb's", "Avtovo");
            Shop shop2 = _shopManager.AddShop("Renat's", "Yanino");

            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            var list = new List<Lot>
            {
                new Lot(orange, 0, 50),
                new Lot(banana, 0, 51)
            };
            
            Shop cheapest = _shopManager.FindCheapestShop(list);
            Assert.AreEqual(null, cheapest);
        }
        
        [Test]
        public void TryFindCheapestShop_NoShops_CheapestIsNull()
        {
            Product orange = _shopManager.RegisterProduct("apelsin");
            Product banana = _shopManager.RegisterProduct("banana");

            var list = new List<Lot>
            {
                new Lot(orange, 0, 50),
                new Lot(banana, 0, 51)
            };
            
            Shop cheapest = _shopManager.FindCheapestShop(list);
            Assert.AreEqual(null, cheapest);
        }
    }
}