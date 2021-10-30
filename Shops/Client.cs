using System;
using System.Collections.Generic;
using System.Linq;

namespace Shops
{
    public class Client
    {
        public Client(string name, uint money, Guid id)
        {
            Name = name;
            Money = (uint)money;
            ID = id;
        }

        public string Name { get; }
        public uint Money { get; }
        public Guid ID { get; }

        public ClientBuilder ToBuilder()
        {
            return new ClientBuilder(this);
        }

        public class ClientBuilder
        {
            private string _name;
            private uint _money;
            private Guid _id;

            public ClientBuilder(Client person)
            {
                _name = person.Name;
                _money = person.Money;
                _id = person.ID;
            }

            public ClientBuilder WithMoney(uint money)
            {
                _money = money;
                return this;
            }

            public Client Build()
            {
                return new Client(_name, _money, _id);
            }
        }
    }
}