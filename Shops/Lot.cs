namespace Shops
{
    public class Lot
    {
        public Lot(Product product, uint price, uint amount)
        {
            Product = product;
            Price = price;
            Amount = amount;
        }

        public Product Product { get; }

        public uint Price { get; }

        public uint Amount { get; }

        public LotBuilder ToBuilder()
        {
            return new LotBuilder(this);
        }

        public class LotBuilder
        {
            private Product _product;
            private uint _price;
            private uint _amount;

            public LotBuilder(Lot lot)
            {
                _product = lot.Product;
                _price = lot.Price;
                _amount = lot.Amount;
            }

            public LotBuilder WithPrice(uint price)
            {
                _price = price;
                return this;
            }

            public LotBuilder WithAmount(uint amount)
            {
                _amount = amount;
                return this;
            }

            public LotBuilder AddAmount(uint amount)
            {
                _amount += amount;
                return this;
            }

            public LotBuilder RemoveAmount(uint amount)
            {
                _amount -= amount;
                return this;
            }

            public Lot Build()
            {
                return new Lot(_product, _price, _amount);
            }
        }
    }
}