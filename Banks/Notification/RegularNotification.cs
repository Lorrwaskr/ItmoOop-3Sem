using System;
using Banks.Client;

namespace Banks.Notification
{
    public class RegularNotification : INotification
    {
        public void Notify(IClient client, string address)
        {
            Console.WriteLine(address);
        }
    }
}